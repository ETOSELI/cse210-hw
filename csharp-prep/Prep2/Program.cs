using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter your grade percentage: ");
        string input = Console.ReadLine();

        // Convert input to a double
        double gradePercentage = double.Parse(input);

        // Determine letter grade
        char letter;

        if (gradePercentage >= 90)
        {
            letter = 'A';
        }
        else if (gradePercentage >= 80)
        {
            letter = 'B';
        }
        else if (gradePercentage >= 70)
        {
            letter = 'C';
        }
        else if (gradePercentage >= 60)
        {
            letter = 'D';
        }
        else
        {
            letter = 'F';
        }

        // Determine pass/fail
        string result;

        if (gradePercentage >= 70)
        {
            result = "Congratulations, you passed!";
        }
        else
        {
            result = "Sorry, you did not pass. Better luck next time!";
        }

        // Determine grade modifier (+/-)
        char modifier = ' ';

        int lastDigit = (int)gradePercentage % 10;

        if (lastDigit >= 7 && letter != 'A' && letter != 'F')
        {
            modifier = '+';
        }
        else if (lastDigit < 3 && letter != 'F')
        {
            modifier = '-';
        }

        // Output final result
        Console.WriteLine($"Your grade is {letter}{modifier}. {result}");

        // Keep the console window open until a key is pressed (optional)
        Console.ReadKey();
    }
}
