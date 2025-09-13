// Entry.cs
using System;

public class Entry
{
    public string _date;
    public string _prompt;
    public string _response;

    // Constructor
    public Entry(string date, string prompt, string response)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
    }

    // Method to display the entry
    public void DisplayEntry()
    {
        Console.WriteLine($"Date: {_date} - Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}");
        Console.WriteLine(); // Add a blank line for readability
    }
}