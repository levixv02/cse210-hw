using System;
using System.Collections.Generic;

abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public DateTime GetDate()
    {
        return _date;
    }

    public int GetMinutes()
    {
        return _minutes;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{_date:dd MMM yyyy} {GetType().Name.Replace("Activity", "")} ({_minutes} min): Distance {GetDistance():F1} km, Speed {GetSpeed():F1} kph, Pace {GetPace():F2} min per km";
    }
}

class RunningActivity : Activity
{
    private double _distance;

    public RunningActivity(DateTime date, int minutes, double distance)
        : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return (_distance / GetMinutes()) * 60;
    }

    public override double GetPace()
    {
        return GetMinutes() / _distance;
    }
}

class CyclingActivity : Activity
{
    private double _speed;

    public CyclingActivity(DateTime date, int minutes, double speed)
        : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return (_speed * GetMinutes()) / 60;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        return 60 / _speed;
    }
}

class SwimmingActivity : Activity
{
    private int _laps;

    public SwimmingActivity(DateTime date, int minutes, int laps)
        : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        return (_laps * 50) / 1000.0;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / GetMinutes()) * 60;
    }

    public override double GetPace()
    {
        return GetMinutes() / GetDistance();
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        activities.Add(
            new RunningActivity(
                new DateTime(2022, 11, 3),
                30,
                4.8));

        activities.Add(
            new CyclingActivity(
                new DateTime(2022, 11, 4),
                45,
                25.0));

        activities.Add(
            new SwimmingActivity(
                new DateTime(2022, 11, 5),
                40,
                30));

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}