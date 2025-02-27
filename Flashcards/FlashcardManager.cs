using Flashcards.Controllers;
using Flashcards.Models;
using Spectre.Console;

namespace Flashcards;

internal class FlashcardManager
{
    private FlashcardController flashcardController = new();
    private CardStackController cardStackController = new();
    private int stackId;
    private string stackName;
    private int flashcardId;

    internal void DisplayFlashcardOptions()
    {
        bool loop = true;
        Console.Clear();
        GetStackChoice();
        while (loop)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"Current stack: [blue]{stackName}[/]");
            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose an option:")
            .AddChoices(["Show flashcards", "Add flashcards", "Edit a flashcard", "Delete a flashcard", "Return"]));

            switch (choice)
            {
                case "Show flashcards":
                    DisplayFlashcards(stackId);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "Add flashcards":
                    int amount = UserInput.getIntInput("Enter the amount of flashcards you want to add: ");
                    for (int i = 1; i <= amount; i++)
                    {
                        Console.WriteLine($"Adding flashcard number {i}: ");
                        (String Term, String Definition) flashcard = UserInput.GetFlashcardInput();
                        flashcardController.AddFlashcard(flashcard.Term, flashcard.Definition, stackId);
                    }
                    break;
                case "Edit a flashcard":
                    DisplayFlashcards(stackId);
                    Console.WriteLine("Enter the ID of the flashcard you want to edit or type 0 to return: ");
                    flashcardId = UserInput.GetFlashcardId(flashcardController, stackId);
                    if (flashcardId != 0)
                    {
                        (String Term, String Definition) newFlashcard = UserInput.GetFlashcardInput(edit: true);
                        flashcardController.EditFlashcard(newFlashcard.Term, newFlashcard.Definition, stackId, flashcardId);
                    }
                    break;
                case "Delete a flashcard":
                    DisplayFlashcards(stackId);
                    Console.WriteLine("Enter the ID of the flashcard you want to delete or type 0 to return: ");
                    flashcardId = UserInput.GetFlashcardId(flashcardController, stackId);
                    if (flashcardId != 0)
                    {
                        HandleRemoval(stackId);
                    }
                    break;
                case "Return":
                    loop = false;
                    break;
            }
        }
    }

    internal void GetStackChoice()
    {
        var stackMap = cardStackController.GetStackNameToIdMap();
        var stackChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select the stack:")
            .AddChoices(stackMap.Keys));
        stackName = stackChoice;
        stackId = stackMap[stackChoice];
    }

    internal void DisplayFlashcards(int stackId)
    {
        var flashcards = flashcardController.GetFlashcards(stackId);
        if (flashcards.Count() == 0)
        {
            AnsiConsole.MarkupLine("[red]No flashcards found![/]");
        }
        else
        {
            foreach (var flashcard in flashcards)
            {
                AnsiConsole.MarkupLine($"{flashcard.Id}. [Blue]Term[/]: {flashcard.Term}, [Blue]Definition[/]: {flashcard.Definition}");
            }
        }
    }

    internal void HandleRemoval(int stackId)
    {
        int currentCount = flashcardController.GetFlashcardCount(stackId);
        int idsToChange = currentCount - flashcardId;
        flashcardController.DeleteFlashcard(stackId, flashcardId);
        for (int i = flashcardId + 1; i <= currentCount; i++)
        {
            flashcardController.SetCorrectId(stackId, i);
        }
    }
}