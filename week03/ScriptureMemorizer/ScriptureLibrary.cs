using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions; // Required for Reference.FromString

// Ensure these using statements are present if Reference and Scripture
// are in the same namespace but separate files.
// using ScriptureMemorizer.Reference;
// using ScriptureMemorizer.Scripture;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Manages a collection of Scripture objects, allowing loading from file and random selection.
    /// Also maintains a global library of all unique words found across all loaded scriptures.
    /// </summary>
    public class ScriptureLibrary
    {
        private List<Scripture> _scriptures;
        private HashSet<string> _allUniqueWords; // Stores all unique words from all loaded scriptures
        private Random _random = new Random(); // Instance of Random for the library

        /// <summary>
        /// Initializes an empty ScriptureLibrary.
        /// </summary>
        public ScriptureLibrary()
        {
            _scriptures = new List<Scripture>();
            _allUniqueWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase); // Case-insensitive unique words
        }

        /// <summary>
        /// Adds a Scripture object to the library.
        /// When a scripture is added, its words are also added to the global unique word list.
        /// </summary>
        /// <param name="scripture">The Scripture object to add.</param>
        public void AddScripture(Scripture scripture)
        {
            _scriptures.Add(scripture);
            // Add all words from the new scripture to the global unique word set
            foreach (var word in scripture.GetAllWords())
            {
                _allUniqueWords.Add(word.GetOriginalText());
            }
        }

        /// <summary>
        /// Returns a random Scripture object from the library.
        /// </summary>
        /// <returns>A randomly selected Scripture object.</returns>
        /// <exception cref="InvalidOperationException">If the library is empty.</exception>
        public Scripture GetRandomScripture()
        {
            if (!_scriptures.Any())
            {
                throw new InvalidOperationException("Scripture library is empty. Please load scriptures first.");
            }
            return _scriptures[_random.Next(_scriptures.Count)];
        }

        /// <summary>
        /// Loads scriptures from a text file. Each line should be in the format:
        /// "Reference|Text" (e.g., "John 3:16|For God so loved the world...")
        /// </summary>
        /// <param name="filename">The path to the file containing scriptures.</param>
        /// <exception cref="IOException">If the file cannot be opened or read.</exception>
        public void LoadFromFile(string filename)
        {
            try
            {
                foreach (string line in File.ReadLines(filename))
                {
                    string trimmedLine = line.Trim();
                    if (string.IsNullOrEmpty(trimmedLine))
                    {
                        continue; // Skip empty lines
                    }

                    string[] parts = trimmedLine.Split('|', 2); // Split only on the first '|'
                    if (parts.Length != 2)
                    {
                        Console.WriteLine($"Warning: Skipping invalid line format: {trimmedLine}");
                        continue;
                    }

                    string refStr = parts[0].Trim();
                    string text = parts[1].Trim();
                    try
                    {
                        Reference reference = Reference.FromString(refStr);
                        Scripture scripture = new Scripture(reference, text);
                        AddScripture(scripture); // This will now also add words to _allUniqueWords
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"Warning: Could not parse scripture from line '{trimmedLine}'. Error: {e.Message}");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: File '{filename}' not found.");
                throw new IOException($"File '{filename}' not found.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while loading scriptures from '{filename}': {e.Message}");
                throw new IOException($"Error reading file '{filename}': {e.Message}");
            }
        }

        /// <summary>
        /// Returns a list of all unique words collected from all loaded scriptures.
        /// </summary>
        /// <returns>A List of unique word strings.</returns>
        public List<string> GetAllUniqueWords()
        {
            return _allUniqueWords.ToList();
        }
    }
}