using System;
using System.Collections.Generic;
using System.IO;

// W02 PROJECT: JOURNAL PROGRAM

public class Entry
{
    public string _date;
    public string _promptText;
    public string _entryText;

    public void Display()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_promptText}");
        Console.WriteLine(_entryText);
        Console.WriteLine();
    }
}


// PROMPT GENERATOR CLASS

public class PromptGenerator
{
    public List<string> _prompts = new List<string>();

    public PromptGenerator()
    {
        _prompts.Add(
            "Who was the most interesting person I interacted with today?"
        );

        _prompts.Add(
            "What was the best part of my day?"
        );

        _prompts.Add(
            "How did I see the hand of the Lord in my life today?"
        );

        _prompts.Add(
            "What was the strongest emotion I felt today?"
        );

        _prompts.Add(
            "If I had one thing I could do over today, what would it be?"
        );

        _prompts.Add(
            "What is one thing I learned today?"
        );

        _prompts.Add(
            "What am I grateful for today?"
        );
    }

    public string GetRandomPrompt()
    {
        Random random = new Random();

        int index = random.Next(_prompts.Count);

        return _prompts[index];
    }
}

// JOURNAL CLASS

public class Journal
{
    public List<Entry> _entries = new List<Entry>();


    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine();
            Console.WriteLine("Your journal is currently empty.");
            Console.WriteLine();

            return;
        }

        Console.WriteLine();

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }


    public void SaveToFile(string file)
    {
        using (StreamWriter outputFile = new StreamWriter(file))
        {
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine(
                    $"{entry._date}~|~{entry._promptText}~|~{entry._entryText}"
                );
            }
        }
    }


    public void LoadFromFile(string file)
    {
        _entries.Clear();

        string[] lines = File.ReadAllLines(file);

        foreach (string line in lines)
        {
            string[] parts = line.Split("~|~");

            Entry entry = new Entry();

            entry._date = parts[0];
            entry._promptText = parts[1];
            entry._entryText = parts[2];

            _entries.Add(entry);
        }
    }
}


// PROGRAM CLASS


class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        PromptGenerator promptGenerator =
            new PromptGenerator();

        bool running = true;

        Console.WriteLine(
            "Welcome to the Journal Program!"
        );


        while (running)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Please select one of the following choices:"
            );

            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");

            Console.Write(
                "What would you like to do? "
            );

            string choice = Console.ReadLine();


            if (choice == "1")
            {
                string prompt =
                    promptGenerator.GetRandomPrompt();

                Console.WriteLine();
                Console.WriteLine(prompt);

                Console.Write("> ");

                string response =
                    Console.ReadLine();


                DateTime currentTime =
                    DateTime.Now;

                string dateText =
                    currentTime.ToShortDateString();


                Entry entry =
                    new Entry();

                entry._date =
                    dateText;

                entry._promptText =
                    prompt;

                entry._entryText =
                    response;


                journal.AddEntry(entry);

                Console.WriteLine();
                Console.WriteLine(
                    "Your journal entry was added."
                );
            }


            else if (choice == "2")
            {
                journal.DisplayAll();
            }


            else if (choice == "3")
            {
                Console.Write(
                    "What is the filename? "
                );

                string filename =
                    Console.ReadLine();

                if (File.Exists(filename))
                {
                    journal.LoadFromFile(
                        filename
                    );

                    Console.WriteLine(
                        "Journal loaded successfully."
                    );
                }

                else
                {
                    Console.WriteLine(
                        "The file could not be found."
                    );
                }
            }


            else if (choice == "4")
            {
                Console.Write(
                    "What is the filename? "
                );

                string filename =
                    Console.ReadLine();

                journal.SaveToFile(
                    filename
                );

                Console.WriteLine(
                    "Journal saved successfully."
                );
            }


            else if (choice == "5")
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Thank you for using the Journal Program."
                );

                running = false;
            }


            else
            {
                Console.WriteLine();

                Console.WriteLine(
                    "Invalid choice. Please choose a number from 1 to 5."
                );
            }
        }
    }
}