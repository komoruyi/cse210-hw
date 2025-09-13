// Program.cs
using System;

class Program

/*
Author: Kenneth Osaohenmwanta Omoruyi
Purpose: Journal app
Exceeding Requirements:

I have exceeded the core requirements by:
- Adding more than five prompts to the PromptGenerator class.
- Implementing basic input validation for the menu choice in the Program class to handle non-numeric inputs gracefully.
- Providing user-friendly messages for file operations (e.g., "File not found" or "Entry added successfully!").
*/

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Journal Project.");
        Journal myJournal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();
        int choice = 0;

        Console.WriteLine("Welcome to the Journal Program!");

        while (choice != 5) // Option 5 will be "Quit"
        {
            Console.WriteLine("--- Journal Menu ---");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("Please enter your choice: ");

            // Input validation for choice
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1: // Write a new entry
                        string randomPrompt = promptGenerator.GetRandomPrompt();
                        Console.WriteLine($"Prompt: {randomPrompt}");
                        Console.Write("Your response: ");
                        string userResponse = Console.ReadLine();
                        string currentDate = DateTime.Now.ToShortDateString(); // Get current date

                        Entry newEntry = new Entry(currentDate, randomPrompt, userResponse);
                        myJournal.AddEntry(newEntry);
                        Console.WriteLine("Entry added successfully!");
                        break;

                    case 2: // Display the journal
                        myJournal.DisplayJournal();
                        break;

                    case 3: // Save the journal
                        Console.Write("Enter filename to save (e.g., myjournal.txt): ");
                        string saveFilename = Console.ReadLine();
                        myJournal.SaveJournal(saveFilename);
                        break;

                    case 4: // Load the journal
                        Console.Write("Enter filename to load (e.g., myjournal.txt): ");
                        string loadFilename = Console.ReadLine();
                        myJournal.LoadJournal(loadFilename);
                        break;

                    case 5: // Quit
                        Console.WriteLine("Thank you for using the Journal Program. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            Console.WriteLine(); // Blank line for better readability between menu cycles
        }
    }
}


