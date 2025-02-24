using Flashcards.Controllers;

namespace Flashcards
{
    internal static class UserInput
    {

        internal static string getStringInput(string message)
        {
            string input;
            Console.WriteLine(message);
            do
            {
                input = Console.ReadLine();
                if (input == "")
                {
                    Console.WriteLine("Input cannot be empty! Try again: ");
                }
            }
            while (input == "");
            return input;
        }

        internal static int getIntInput(string message)
        {
            int input;
            Console.WriteLine(message);
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input! Try again: ");
            }
            return input;
        }

        internal static int GetCorrectStackId(CardStackController cardStackController, int id)
        {
            while (!cardStackController.StackExists(id))
            {
                id = UserInput.getIntInput("Stack with this ID does not exist! Try again: ");
            }
            return id;
        }
    }
}