using Flashcards.Controllers;
using Spectre.Console;

namespace Flashcards;

internal class UserInterface
{
    private CardStackController cardStackController  = new();
    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Welcome to [green]Flashcards App[/]! Choose an option:")
                .AddChoices(new[] { "View stacks", "Create new stack", "Edit a stack", "Delete a stack", "Exit the app"}));

            switch (choice)
            {
                case "View stacks":
                    cardStackController.AddStack();
                    break;
                case "Create new stack":
                    break;
                case "Edit a stack":
                    break;
                case "Delete a stack":
                    break;
                case "Exit the app":
                    return;
            }
        }
    }
}