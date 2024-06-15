using System;

class Program
{
    static void Main()
    {
        // Prompt the user for first name
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();

        // Prompt the user for last name
        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();

        // Display the formatted name
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");

        // Keep the console window open until a key is pressed (optional)
        Console.ReadKey();
    }
}
