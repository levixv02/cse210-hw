using System;
using System.Collections.Generic;
using System.Threading;

class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
        _duration = 0;
    }

    public void DisplayStartingMessage()
    {
        Console.Clear();

        Console.WriteLine($"Welcome to the {_name}.\n");
        Console.WriteLine(_description);

        Console.Write("\nHow long, in seconds, would you like for your session? ");

        while (!int.TryParse(Console.ReadLine(), out _duration) || _duration <= 0)
        {
            Console.Write("Please enter a positive number of seconds: ");
        }

        Console.Clear();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
        Console.WriteLine();
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine("\nWell done!!");
        ShowSpinner(2);

        Console.WriteLine(
            $"\nYou have completed another {_duration} seconds of the {_name}."
        );

        ShowSpinner(3);
        Console.WriteLine();
    }

    public void ShowSpinner(int seconds)
    {
        string[] spinner = { "|", "/", "-", "\\" };

        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(spinner[i]);

            Thread.Sleep(250);

            Console.Write("\b \b");

            i++;

            if (i >= spinner.Length)
            {
                i = 0;
            }
        }
    }

    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);

            Thread.Sleep(1000);

            for (int j = 0; j < i.ToString().Length; j++)
            {
                Console.Write("\b \b");
            }
        }
    }

    public int GetDuration()
    {
        return _duration;
    }
}


class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through " +
            "breathing in and out slowly. Clear your mind and focus on your breathing."
        )
    {
    }

    public void Run()
    {
        DisplayStartingMessage();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            Console.Write("\nBreathe in... ");
            ShowCountDown(4);

            if (DateTime.Now >= endTime)
            {
                break;
            }

            Console.Write("\nBreathe out... ");
            ShowCountDown(6);

            Console.WriteLine();
        }

        DisplayEndingMessage();
    }
}


class ReflectingActivity : Activity
{
    private List<string> _prompts;
    private List<string> _questions;
    private Random _random;

    public ReflectingActivity()
        : base(
            "Reflecting Activity",
            "This activity will help you reflect on times in your life when " +
            "you have shown strength and resilience. This will help you recognize " +
            "the power you have and how you can use it in other aspects of your life."
        )
    {
        _random = new Random();

        _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new List<string>
        {
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
    }

    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }

    public string GetRandomQuestion()
    {
        int index = _random.Next(_questions.Count);
        return _questions[index];
    }

    public void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("\nConsider the following prompt:\n");
        Console.WriteLine($" --- {GetRandomPrompt()} --- ");

        Console.WriteLine(
            "\nWhen you have something in mind, press Enter to continue."
        );

        Console.ReadLine();

        Console.WriteLine(
            "\nNow ponder on each of the following questions as they relate to this experience."
        );

        Console.Write("\nYou may begin in: ");
        ShowCountDown(5);

        Console.WriteLine();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            Console.Write($"\n> {GetRandomQuestion()} ");
            ShowSpinner(5);
        }

        DisplayEndingMessage();
    }
}


class ListingActivity : Activity
{
    private List<string> _prompts;
    private Random _random;

    public ListingActivity()
        : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life " +
            "by having you list as many things as you can in a certain area."
        )
    {
        _random = new Random();

        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
    }

    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }

    public void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine(
            "\nList as many responses as you can to the following prompt:"
        );

        Console.WriteLine($"\n --- {GetRandomPrompt()} --- ");

        Console.Write("\nYou may begin in: ");
        ShowCountDown(5);

        Console.WriteLine();

        int count = 0;

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");

            string item = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(item))
            {
                count++;
            }
        }

        Console.WriteLine($"\nYou listed {count} items!");

        DisplayEndingMessage();
    }
}


class Program
{
    static void Main(string[] args)
    {
        string choice = "";

        while (choice != "4")
        {
            Console.Clear();

            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");

            Console.Write("Select a choice from the menu: ");

            choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    BreathingActivity breathingActivity =
                        new BreathingActivity();

                    breathingActivity.Run();
                    break;

                case "2":
                    ReflectingActivity reflectingActivity =
                        new ReflectingActivity();

                    reflectingActivity.Run();
                    break;

                case "3":
                    ListingActivity listingActivity =
                        new ListingActivity();

                    listingActivity.Run();
                    break;

                case "4":
                    Console.WriteLine(
                        "\nThank you for using the Mindfulness Program."
                    );
                    break;

                default:
                    Console.WriteLine(
                        "\nInvalid choice. Please select 1, 2, 3, or 4."
                    );

                    Thread.Sleep(2000);
                    break;
            }
        }
    }
}