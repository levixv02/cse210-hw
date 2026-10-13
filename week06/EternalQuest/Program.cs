using System;
using System.Collections.Generic;
using System.IO;


abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string GetName() => _name;

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();
}

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }

        return 0;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetailsString()
    {
        return $"[{(_isComplete ? "X" : " ")}] {_name} ({_description})";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return _points;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetailsString()
    {
        return $"[∞] {_name} ({_description})";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{_name}|{_description}|{_points}";
    }
}

class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name,
                         string description,
                         int points,
                         int target,
                         int bonus,
                         int amountCompleted = 0)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = amountCompleted;
    }

    public override int RecordEvent()
    {
        if (_amountCompleted < _target)
        {
            _amountCompleted++;

            if (_amountCompleted == _target)
            {
                return _points + _bonus;
            }

            return _points;
        }

        return 0;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        return $"[{(IsComplete() ? "X" : " ")}] {_name} ({_description}) -- Completed {_amountCompleted}/{_target}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_name}|{_description}|{_points}|{_bonus}|{_target}|{_amountCompleted}";
    }
}

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void Start()
    {
        while (true)
        {
            Console.Clear();

            DisplayPlayerInfo();

            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Quit");

            Console.Write("Select a choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;

                case "2":
                    ListGoalDetails();
                    break;

                case "3":
                    RecordEvent();
                    break;

                case "4":
                    SaveGoals();
                    break;

                case "5":
                    LoadGoals();
                    break;

                case "6":
                    return;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        int level = (_score / 1000) + 1;

        Console.WriteLine($"Score: {_score}");
        Console.WriteLine($"Level: {level}");
    }

    public void CreateGoal()
    {
        Console.WriteLine();
        Console.WriteLine("Goal Types:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        Console.Write("Choice: ");
        string choice = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        if (choice == "1")
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (choice == "2")
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else if (choice == "3")
        {
            Console.Write("Target Count: ");
            int target = int.Parse(Console.ReadLine());

            Console.Write("Bonus Points: ");
            int bonus = int.Parse(Console.ReadLine());

            _goals.Add(new ChecklistGoal(
                name,
                description,
                points,
                target,
                bonus));
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine();

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }

        Pause();
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available.");
            Pause();
            return;
        }

        Console.WriteLine();

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }

        Console.Write("Goal Number: ");
        int goalNumber = int.Parse(Console.ReadLine());

        int pointsEarned =
            _goals[goalNumber - 1].RecordEvent();

        _score += pointsEarned;

        Console.WriteLine($"You earned {pointsEarned} points!");
        Pause();
    }

    public void SaveGoals()
    {
        Console.Write("Filename: ");
        string filename = Console.ReadLine();

        using (StreamWriter output = new StreamWriter(filename))
        {
            output.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                output.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Saved successfully.");
        Pause();
    }

    public void LoadGoals()
    {
        Console.Write("Filename: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            Pause();
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        _goals.Clear();

        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');

            string type = parts[0];

            if (type == "SimpleGoal")
            {
                _goals.Add(new SimpleGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3]),
                    bool.Parse(parts[4])));
            }
            else if (type == "EternalGoal")
            {
                _goals.Add(new EternalGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3])));
            }
            else if (type == "ChecklistGoal")
            {
                _goals.Add(new ChecklistGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3]),
                    int.Parse(parts[5]),
                    int.Parse(parts[4]),
                    int.Parse(parts[6])));
            }
        }

        Console.WriteLine("Loaded successfully.");
        Pause();
    }

    private void Pause()
    {
        Console.WriteLine();
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}