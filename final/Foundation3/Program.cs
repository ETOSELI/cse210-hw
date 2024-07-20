using System;

class Address
{
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public Address(string streetAddress, string city, string state, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        State = state;
        Country = country;
    }

    public override string ToString()
    {
        return $"{StreetAddress}, {City}, {State}, {Country}";
    }
}

class Event
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public Address Address { get; set; }

    public Event(string title, string description, DateTime date, string time, Address address)
    {
        Title = title;
        Description = description;
        Date = date;
        Time = time;
        Address = address;
    }

    public virtual string GetStandardDetails()
    {
        return $"Title: {Title}\nDescription: {Description}\nDate: {Date.ToShortDateString()}\nTime: {Time}\nAddress: {Address}";
    }

    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }

    public virtual string GetShortDescription()
    {
        return $"Event Type: {GetType().Name}\nTitle: {Title}\nDate: {Date.ToShortDateString()}";
    }
}

class Lecture : Event
{
    public string Speaker { get; set; }
    public int Capacity { get; set; }

    public Lecture(string title, string description, DateTime date, string time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        Speaker = speaker;
        Capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nSpeaker: {Speaker}\nCapacity: {Capacity}";
    }
}

class Reception : Event
{
    public string RSVPEmail { get; set; }

    public Reception(string title, string description, DateTime date, string time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        RSVPEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nRSVP Email: {RSVPEmail}";
    }
}

class OutdoorGathering : Event
{
    public string WeatherForecast { get; set; }

    public OutdoorGathering(string title, string description, DateTime date, string time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        WeatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nWeather Forecast: {WeatherForecast}";
    }
}

class Program
{
    static void Main()
    {
        // Create addresses
        Address address1 = new Address("123 Main St", "CityA", "StateA", "CountryA");
        Address address2 = new Address("456 Another St", "CityB", "StateB", "CountryB");
        Address address3 = new Address("789 Different St", "CityC", "StateC", "CountryC");

        // Create events
        Lecture lecture = new Lecture("Lecture 1", "A fascinating lecture on C#.", new DateTime(2024, 7, 20), "10:00 AM", address1, "John Doe", 100);
        Reception reception = new Reception("Reception 1", "A networking event.", new DateTime(2024, 7, 21), "6:00 PM", address2, "rsvp@example.com");
        OutdoorGathering outdoorGathering = new OutdoorGathering("Outdoor Gathering 1", "A fun outdoor event.", new DateTime(2024, 7, 22), "2:00 PM", address3, "Sunny");

        // Store events in a list
        List<Event> events = new List<Event> { lecture, reception, outdoorGathering };

        // Display event information
        foreach (var evt in events)
        {
            Console.WriteLine(evt.GetStandardDetails());
            Console.WriteLine();
            Console.WriteLine(evt.GetFullDetails());
            Console.WriteLine();
            Console.WriteLine(evt.GetShortDescription());
            Console.WriteLine();
        }
    }
}