using Flashcards.Controllers;
using Spectre.Console;

namespace Flashcards
{
    internal class CardStackManager
    {
        private CardStackController cardStackController = new();
        private int id;
        private string stackName;

        internal void DisplayStackOptions()
        {
            Console.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose an option:")
                .AddChoices(new[] { "Show stacks", "Add a stack", "Edit a stack", "Delete a stack" }));

            switch (choice)
            {
                case "Show stacks":
                    cardStackController.DisplayStacks();
                    break;
                case "Add a stack":
                    stackName = UserInput.getStringInput("Enter name of the new stack or enter 0 to return: ");
                    if (stackName != "0")
                    {
                        cardStackController.AddStack(stackName);
                    }
                    break;
                case "Edit a stack":
                    cardStackController.DisplayStacks();
                    id = UserInput.getIntInput("Enter ID of the stack you want to edit or enter 0 to return");
                    if (id != 0)
                    {
                        UserInput.GetCorrectStackId(cardStackController, id);
                        string name = UserInput.getStringInput("Enter new name for the stack: ");
                        cardStackController.EditStack(id, name);
                    }
                    break;
                case "Delete a stack":
                    cardStackController.DisplayStacks();
                    id = UserInput.getIntInput("Enter ID of the stack you want to delete or enter 0 to return");
                    if (id != 0)
                    {
                        UserInput.GetCorrectStackId(cardStackController, id);
                        cardStackController.DeleteStack(id);
                    }
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
