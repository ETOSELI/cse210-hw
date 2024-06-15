using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (true)
        {
            Console.Write("Enter number: ");
            int input = int.Parse(Console.ReadLine());

            if (input == 0)
                break;

            numbers.Add(input);
        }

        // Calculate sum
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }

        // Calculate average
        double average = (double)sum / numbers.Count;

        // Find maximum number
        int max = int.MinValue;
        foreach (int number in numbers)
        {
            if (number > max)
                max = number;
        }

        // Output results
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        // Stretch challenge: Find smallest positive number
        int minPositive = int.MaxValue;
        foreach (int number in numbers)
        {
            if (number > 0 && number < minPositive)
                minPositive = number;
        }

        if (minPositive == int.MaxValue)
        {
            Console.WriteLine("No positive numbers were entered.");
        }
        else
        {
            Console.WriteLine($"The smallest positive number is: {minPositive}");
        }

        // Stretch challenge: Sort and display the list
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}
