using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//Author : Omoruyi Kenneth Osaohenmwanta
//Purpose: Scripture Memorizer
//Creativity: Showed creativity by choosing scriptures randomly from library
using ScriptureMemorizer; 

namespace ScriptureMemorizer
{
    /// <summary>
    /// The main program class containing the entry point (Main method).
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Clears the console screen.
        /// </summary>
        static void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// The entry point of the Scripture Memorizer application.
        /// </summary>
        /// <param name="args">Command-line arguments (not used in this program).</param>
        static void Main(string[] args)
        {
            ClearScreen();
            Console.WriteLine("Welcome to the Scripture Memorizer!");
            Console.WriteLine("Loading scriptures...");

            // Define the filename for scriptures
            string scriptureFilename = "scriptures.txt";

            // Create a dummy scripture file for demonstration if it doesn't exist
            if (!File.Exists(scriptureFilename))
            {
                try
                {
                    File.WriteAllText(scriptureFilename,
                        "John 3:16|For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.\n" +
                        "Proverbs 3:5-6|Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.\n" +
                        "Philippians 4:13|I can do all things through Christ which strengtheneth me.\n"
                    );
                    Console.WriteLine($"Created a sample '{scriptureFilename}' file.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating sample scripture file: {ex.Message}");
                    Console.WriteLine("Exiting program.");
                    return;
                }
            }

            // Initialize the Scripture Library and load scriptures
            ScriptureLibrary library = new ScriptureLibrary();
            try
            {
                library.LoadFromFile(scriptureFilename);
            }
            catch (IOException)
            {
                // If file loading fails, exit the program
                Console.WriteLine("Exiting program due to file loading error.");
                return;
            }

            // Get a random scripture to start with
            Scripture currentScripture;
            try
            {
                currentScripture = library.GetRandomScripture();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Please add scriptures to your library file and restart.");
                return;
            }

            // Use a single Random instance for the main loop for hiding words
            Random globalRandom = new Random(); 
            // Get the global list of unique words from the library
            List<string> allWordsFromLibrary = library.GetAllUniqueWords();

            // Main program loop
            while (true)
            {
                ClearScreen();
                // Display the scripture reference and text
                Console.WriteLine($"{currentScripture.GetReference().GetDisplayText()}");
                Console.WriteLine(currentScripture.GetDisplayText());
                Console.WriteLine("\nPress Enter to hide more words, or type 'quit' to exit.");

                // Read user input
                string userInput = Console.ReadLine()?.Trim().ToLower();

                if (userInput == "quit")
                {
                    // User wants to quit, break the loop
                    break;
                }
                else if (string.IsNullOrEmpty(userInput))
                {
                    // User pressed Enter
                    if (currentScripture.IsCompletelyHidden())
                    {
                        // All words are hidden, display final state and end
                        ClearScreen();
                        Console.WriteLine($"{currentScripture.GetReference().GetDisplayText()}");
                        Console.WriteLine(currentScripture.GetDisplayText()); // Display fully hidden scripture
                        Console.WriteLine("\nAll words are hidden! Great job!");
                        break;
                    }
                    else
                    {
                        // Pass the global word pool to HideRandomWords
                        // Hide 2-5 words at a time (inclusive of 2, exclusive of 6)
                        currentScripture.HideRandomWords(globalRandom.Next(2, 6), allWordsFromLibrary);
                    }
                }
                else
                {
                    // Invalid input, inform user and pause
                    Console.WriteLine("Invalid input. Please press Enter or type 'quit'.");
                    Console.WriteLine("Press Enter to continue..."); 
                    Console.ReadLine(); // Wait for user to acknowledge
                }
            }

            Console.WriteLine("\nThank you for using the Scripture Memorizer!");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(); // Keep console open until user presses a key
        }
    }
}