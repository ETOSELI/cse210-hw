using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public bool IsComplete { get; protected set; }

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        IsComplete = false;
    }

    public abstract void RecordEvent();
    public abstract string GetStringRepresentation();

    public void SetComplete(bool isComplete)
    {
        IsComplete = isComplete;
    }
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points) { }

    public override void RecordEvent()
    {
        if (!IsComplete)
        {
            IsComplete = true;
            Console.WriteLine($"Completed {Name}, gained {Points} points!");
        }
        else
        {
            Console.WriteLine($"{Name} is already completed.");
        }
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{Name},{Description},{Points},{IsComplete}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded {Name}, gained {Points} points!");
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{Name},{Description},{Points}";
    }
}

class ChecklistGoal : Goal
{
    public int TargetCount { get; private set; }
    public int CurrentCount { get; private set; }
    public int BonusPoints { get; private set; }

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        TargetCount = targetCount;
        BonusPoints = bonusPoints;
        CurrentCount = 0;
    }

    public override void RecordEvent()
    {
        if (CurrentCount < TargetCount)
        {
            CurrentCount++;
            Console.WriteLine($"Recorded {Name}, gained {Points} points!");

            if (CurrentCount == TargetCount)
            {
                IsComplete = true;
                Console.WriteLine($"Completed {Name}, gained bonus of {BonusPoints} points!");
            }
        }
        else
        {
            Console.WriteLine($"{Name} is already completed.");
        }
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{Name},{Description},{Points},{TargetCount},{CurrentCount},{BonusPoints},{IsComplete}";
    }

    public void SetCurrentCount(int currentCount)
    {
        CurrentCount = currentCount;
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int score = 0;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    RecordEvent();
                    break;
                case "3":
                    DisplayGoals();
                    break;
                case "4":
                    DisplayScore();
                    break;
                case "5":
                    SaveGoals();
                    break;
                case "6":
                    LoadGoals();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Select goal type: ");
        string type = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        Console.Write("Enter points for completing the goal: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                goals.Add(new SimpleGoal(name, description, points));
                break;
            case "2":
                goals.Add(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("Enter target count: ");
                int targetCount = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonusPoints = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, description, points, targetCount, bonusPoints));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }
    }

    static void RecordEvent()
    {
        DisplayGoals();
        Console.Write("Enter goal number to record event: ");
        int goalIndex = int.Parse(Console.ReadLine()) - 1;

        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            goals[goalIndex].RecordEvent();
            if (!goals[goalIndex].IsComplete)
                score += goals[goalIndex].Points;
            else
            {
                if (goals[goalIndex] is ChecklistGoal checklistGoal)
                    score += checklistGoal.BonusPoints;
            }
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    static void DisplayGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            var goal = goals[i];
            string status = goal.IsComplete ? "[X]" : "[ ]";
            if (goal is ChecklistGoal checklistGoal)
            {
                status += $" Completed {checklistGoal.CurrentCount}/{checklistGoal.TargetCount} times";
            }
            Console.WriteLine($"{i + 1}. {status} {goal.Name} - {goal.Description}");
        }
    }

    static void DisplayScore()
    {
        Console.WriteLine($"Current Score: {score}");
    }

    static void SaveGoals()
    {
        using (StreamWriter outputFile = new StreamWriter("goals.txt"))
        {
            outputFile.WriteLine(score);
            foreach (var goal in goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
        Console.WriteLine("Goals saved successfully.");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            string[] lines = File.ReadAllLines("goals.txt");
            score = int.Parse(lines[0]);
            goals.Clear();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':');
                string type = parts[0];
                string[] goalDetails = parts[1].Split(',');

                switch (type)
                {
                    case "SimpleGoal":
                        var simpleGoal = new SimpleGoal(goalDetails[0], goalDetails[1], int.Parse(goalDetails[2]));
                        simpleGoal.SetComplete(bool.Parse(goalDetails[3]));
                        goals.Add(simpleGoal);
                        break;
                    case "EternalGoal":
                        goals.Add(new EternalGoal(goalDetails[0], goalDetails[1], int.Parse(goalDetails[2])));
                        break;
                    case "ChecklistGoal":
                        var checklistGoal = new ChecklistGoal(goalDetails[0], goalDetails[1], int.Parse(goalDetails[2]), int.Parse(goalDetails[3]), int.Parse(goalDetails[4]));
                        checklistGoal.SetCurrentCount(int.Parse(goalDetails[5]));
                        checklistGoal.SetComplete(bool.Parse(goalDetails[6]));
                        goals.Add(checklistGoal);
                        break;
                    default:
                        Console.WriteLine("Unknown goal type.");
                        break;
                }
            }
            Console.WriteLine("Goals loaded successfully.");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}
