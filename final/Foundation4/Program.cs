using System;

abstract class Activity
{
    public DateTime Date { get; set; }
    public int Length { get; set; } // Length in minutes

    protected Activity(DateTime date, int length)
    {
        Date = date;
        Length = length;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{Date.ToShortDateString()} {GetType().Name} ({Length} min) - Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }
}

class Running : Activity
{
    public double Distance { get; set; } // Distance in miles

    public Running(DateTime date, int length, double distance)
        : base(date, length)
    {
        Distance = distance;
    }

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        return (Distance / Length) * 60;
    }

    public override double GetPace()
    {
        return Length / Distance;
    }
}

class Cycling : Activity
{
    public double Speed { get; set; } // Speed in mph

    public Cycling(DateTime date, int length, double speed)
        : base(date, length)
    {
        Speed = speed;
    }

    public override double GetDistance()
    {
        return (Speed * Length) / 60;
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetPace()
    {
        return 60 / Speed;
    }
}

class Swimming : Activity
{
    public int Laps { get; set; } // Number of laps

    public Swimming(DateTime date, int length, int laps)
        : base(date, length)
    {
        Laps = laps;
    }

    public override double GetDistance()
    {
        return Laps * 50 / 1000.0 * 0.62; // Distance in miles
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Length) * 60;
    }

    public override double GetPace()
    {
        return Length / GetDistance();
    }
}

class Program
{
    static void Main()
    {
        // Create activities
        Running running = new Running(new DateTime(2024, 7, 20), 30, 3.0);
        Cycling cycling = new Cycling(new DateTime(2024, 7, 21), 45, 15.0);
        Swimming swimming = new Swimming(new DateTime(2024, 7, 22), 60, 20);

        // Store activities in a list
        List<Activity> activities = new List<Activity> { running, cycling, swimming };

        // Display activity summaries
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}