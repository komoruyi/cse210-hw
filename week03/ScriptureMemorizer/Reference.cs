using System;
using System.Text.RegularExpressions;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents a scripture reference, handling single verses or verse ranges.
    /// </summary>
    public class Reference
    {
        public string Book { get; private set; }
        public int Chapter { get; private set; }
        public int Verse { get; private set; } // Represents the starting verse
        public int? EndVerse { get; private set; } // Nullable int for optional end verse

        /// <summary>
        /// Initializes a Reference object for a single verse.
        /// </summary>
        /// <param name="book">The name of the book (e.g., "John").</param>
        /// <param name="chapter">The chapter number.</param>
        /// <param name="verse">The verse number.</param>
        public Reference(string book, int chapter, int verse)
        {
            Book = book;
            Chapter = chapter;
            Verse = verse;
            EndVerse = null; // No end verse for single verse
        }

        /// <summary>
        /// Initializes a Reference object for a verse range.
        /// </summary>
        /// <param name="book">The name of the book (e.g., "Proverbs").</param>
        /// <param name="chapter">The chapter number.</param>
        /// <param name="startVerse">The starting verse number.</param>
        /// <param name="endVerse">The ending verse number.</param>
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            Book = book;
            Chapter = chapter;
            Verse = startVerse; // Store startVerse in the Verse property
            EndVerse = endVerse;
        }

        /// <summary>
        /// Creates a Reference object by parsing a string (e.g., "John 3:16" or "Proverbs 3:5-6").
        /// </summary>
        /// <param name="refStr">The reference string.</param>
        /// <returns>A new Reference object.</returns>
        /// <exception cref="ArgumentException">If the reference string format is invalid.</exception>
        public static Reference FromString(string refStr)
        {
            Match match = Regex.Match(refStr, @"([A-Za-z\s]+)\s(\d+):(\d+)(?:-(\d+))?");
            if (!match.Success)
            {
                throw new ArgumentException($"Invalid reference string format: {refStr}");
            }

            string book = match.Groups[1].Value.Trim();
            int chapter = int.Parse(match.Groups[2].Value);
            int startVerse = int.Parse(match.Groups[3].Value);
            string endVerseStr = match.Groups[4].Value;

            if (!string.IsNullOrEmpty(endVerseStr))
            {
                int endVerse = int.Parse(endVerseStr);
                return new Reference(book, chapter, startVerse, endVerse);
            }
            else
            {
                return new Reference(book, chapter, startVerse);
            }
        }

        /// <summary>
        /// Returns the formatted string representation of the reference for display.
        /// </summary>
        /// <returns>The formatted reference string (e.g., "John 3:16" or "Proverbs 3:5-6").</returns>
        public string GetDisplayText()
        {
            if (EndVerse.HasValue)
            {
                return $"{Book} {Chapter}:{Verse}-{EndVerse.Value}";
            }
            else
            {
                return $"{Book} {Chapter}:{Verse}";
            }
        }

        /// <summary>
        /// Overrides ToString() to provide a default string representation,
        /// which is often used implicitly by Console.WriteLine.
        /// </summary>
        public override string ToString()
        {
            return GetDisplayText();
        }
    }
}