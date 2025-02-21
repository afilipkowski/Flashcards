namespace Flashcards
{
    internal static class UserInput
    {

        internal static string getStringInput(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            return input;
        }
    }
}
