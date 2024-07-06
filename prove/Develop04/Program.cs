using System;
using System.Threading;

// Base class for all activities
public abstract class MindfulnessActivity
{
    protected int durationSeconds;

    public MindfulnessActivity(int duration)
    {
        this.durationSeconds = duration;
    }

    // Common starting message for all activities
    protected void StartActivity(string activityName, string description)
    {
        Console.WriteLine($"{activityName} Activity");
        Console.WriteLine(description);
        Console.WriteLine($"Duration set to {durationSeconds} seconds.");
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000); // Pause for 3 seconds
    }

    // Common ending message for all activities
    protected void EndActivity(string activityName)
    {
        Console.WriteLine($"Great job on the {activityName} activity!");
        Console.WriteLine($"Activity completed for {durationSeconds} seconds.");
        Thread.Sleep(3000); // Pause for 3 seconds
    }

    // Abstract method to be implemented by derived classes
    public abstract void RunActivity();
}

// Breathing activity class
public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity(int duration) : base(duration)
    {
    }

    public override void RunActivity()
    {
        StartActivity("Breathing", "This activity will help you relax by guiding you through deep breathing exercises. Clear your mind and focus on your breathing.");

        // Alternating messages for breathing
        for (int i = 0; i < durationSeconds; i += 2)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(1000); // Pause for 1 second
            Console.WriteLine("Breathe out...");
            Thread.Sleep(1000); // Pause for 1 second
        }

        EndActivity("Breathing");
    }
}

// Reflection activity class
public class ReflectionActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity(int duration) : base(duration)
    {
    }

    public override void RunActivity()
    {
        StartActivity("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");

        Random random = new Random();

        // Select a random prompt
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);

        // Display random questions
        foreach (string question in questions)
        {
            Console.WriteLine(question);
            Thread.Sleep(3000); // Pause for 3 seconds
        }

        EndActivity("Reflection");
    }
}

// Listing activity class
public class ListingActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(int duration) : base(duration)
    {
    }

    public override void RunActivity()
    {
        StartActivity("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");

        Random random = new Random();

        // Select a random prompt
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);

        Console.WriteLine("You have 10 seconds to begin listing...");

        // Countdown for 10 seconds
        for (int i = 10; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000); // Pause for 1 second
        }
        Console.WriteLine();

        // Simulate listing items (in a real app, you'd have user input here)
        int itemCount = random.Next(5, 15); // Random number of items
        Console.WriteLine($"You listed {itemCount} items.");

        EndActivity("Listing");
    }
}

// Main program
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Mindfulness Program!");

        // Menu system
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            Console.Write("Choose an activity: ");
            string choice = Console.ReadLine();

            MindfulnessActivity activity = null;
            int duration = 0;

            switch (choice)
            {
                case "1":
                    duration = GetDuration();
                    activity = new BreathingActivity(duration);
                    break;
                case "2":
                    duration = GetDuration();
                    activity = new ReflectionActivity(duration);
                    break;
                case "3":
                    duration = GetDuration();
                    activity = new ListingActivity(duration);
                    break;
                case "4":
                    Console.WriteLine("Exiting program...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose again.");
                    continue;
            }

            if (activity != null)
            {
                activity.RunActivity();
            }
        }
    }

    // Helper method to get duration from user
    private static int GetDuration()
    {
        int duration = 0;
        while (true)
        {
            Console.Write("Enter duration in seconds: ");
            if (int.TryParse(Console.ReadLine(), out duration) && duration > 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
        }
        return duration;
    }
}
