using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    // Entry class to represent a single journal entry
    public class Entry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Date { get; set; }

        public Entry(string prompt, string response, string date)
        {
            Prompt = prompt;
            Response = response;
            Date = date;
        }

        public override string ToString()
        {
            return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
        }
    }

    // Journal class to manage multiple entries
    public class Journal
    {
        private List<Entry> entries;

        public Journal()
        {
            entries = new List<Entry>();
        }

        // Method to add a new entry to the journal
        public void AddEntry(Entry entry)
        {
            entries.Add(entry);
        }

        // Method to display all entries in the journal
        public void DisplayEntries()
        {
            foreach (Entry entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        // Method to save the journal to a file
        public void SaveToFile(string filename)
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                foreach (Entry entry in entries)
                {
                    outputFile.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
                }
            }
        }

        // Method to load the journal from a file
        public void LoadFromFile(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            entries.Clear();
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    entries.Add(new Entry(parts[1], parts[2], parts[0]));
                }
            }
        }
    }

    // Program class to handle user interaction
    class Program
    {
        private static List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        private static Random random = new Random();
        private static Journal journal = new Journal();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Journal App Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        WriteNewEntry();
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        SaveJournal();
                        break;
                    case "4":
                        LoadJournal();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        // Method to write a new entry
        static void WriteNewEntry()
        {
            string prompt = prompts[random.Next(prompts.Count)];
            Console.WriteLine($"Prompt: {prompt}");
            Console.Write("Your response: ");
            string response = Console.ReadLine();
            string date = DateTime.Now.ToShortDateString();
            journal.AddEntry(new Entry(prompt, response, date));
        }

        // Method to save the journal
        static void SaveJournal()
        {
            Console.Write("Enter the filename to save the journal: ");
            string filename = Console.ReadLine();
            journal.SaveToFile(filename);
            Console.WriteLine("Journal saved successfully.");
        }

        // Method to load the journal
        static void LoadJournal()
        {
            Console.Write("Enter the filename to load the journal: ");
            string filename = Console.ReadLine();
            journal.LoadFromFile(filename);
            Console.WriteLine("Journal loaded successfully.");
        }
    }
}
