using Flashcards.Controllers;
using Spectre.Console;

namespace Flashcards;

internal class UserInterface
{
    private CardStackManager cardStackManager = new();
    private FlashcardManager flashcardManager = new();
    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Welcome to [green]Flashcards App[/]! Choose an option:")
                .AddChoices(new[] { "Manage stacks", "Manage flashcards", "Study", "Exit the app"}));

            switch (choice)
            {
                case "Manage stacks":  
                    cardStackManager.DisplayStackOptions();
                    break;
                case "Manage flashcards":
                    break;
                case "Study":
                    break;
                case "Exit the app":
                    return;
            }
        }
    }
}