// Journal.cs
using System;
using System.Collections.Generic;
using System.IO; // Required for file operations

public class Journal
{
    public List<Entry> _entries;

    public Journal()
    {
        _entries = new List<Entry>();
    }

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("Your journal is empty. Start by writing a new entry!");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.DisplayEntry();
        }
    }

    public void SaveJournal(string filename)
    {
        // Use a separator that's unlikely to appear in the text
        // For example, "~|~" as suggested in the simplifications
        string separator = "~|~";

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                // Write each part of the entry, separated by the chosen symbol
                outputFile.WriteLine($"{entry._date}{separator}{entry._prompt}{separator}{entry._response}");
            }
        }
        Console.WriteLine($"Journal saved to {filename}");
    }

    public void LoadJournal(string filename)
    {
        // Clear existing entries before loading new ones
        _entries.Clear(); 
        string separator = "~|~";

        if (!File.Exists(filename))
        {
            Console.WriteLine($"File '{filename}' not found. No journal loaded.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split(new string[] { separator }, StringSplitOptions.None);

            if (parts.Length == 3) // Ensure we have all three parts
            {
                string date = parts[0];
                string prompt = parts[1];
                string response = parts[2];
                _entries.Add(new Entry(date, prompt, response));
            }
        }
        Console.WriteLine($"Journal loaded from {filename}");
    }
}