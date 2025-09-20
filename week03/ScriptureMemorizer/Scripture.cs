using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// Ensure these using statements are present if Reference, Word, and ScripturePart
// are in the same namespace but separate files.
// using ScriptureMemorizer.Reference;
// using ScriptureMemorizer.Word;
// using ScriptureMemorizer.ScripturePart;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents a complete scripture, including its reference and a list of words.
    /// Manages the hiding and displaying of words.
    /// </summary>
    public class Scripture
    {
        private Reference _reference;
        private List<ScripturePart> _originalTextParts;
        private Random _random = new Random(); // Instance of Random for this scripture

        /// <summary>
        /// Initializes a Scripture object.
        /// </summary>
        /// <param name="reference">The Reference object for the scripture.</param>
        /// <param name="text">The full text of the scripture.</param>
        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _originalTextParts = new List<ScripturePart>();

            MatchCollection matches = Regex.Matches(text, @"(\w+|[^\w\s]+|\s+)");

            foreach (Match match in matches)
            {
                string part = match.Value;
                if (!string.IsNullOrWhiteSpace(part) && Regex.IsMatch(part, @"^\w+$"))
                {
                    _originalTextParts.Add(new ScripturePart(new Word(part)));
                }
                else
                {
                    _originalTextParts.Add(new ScripturePart(part));
                }
            }
        }

        /// <summary>
        /// Hides a specified number of random, currently unhidden words in the scripture.
        /// Prioritizes words from a global pool if provided, then falls back to local unhidden words.
        /// </summary>
        /// <param name="numberToHide">The number of words to hide.</param>
        /// <param name="globalWordPool">An optional list of unique words from a global library to prioritize selection from.</param>
        public void HideRandomWords(int numberToHide, List<string> globalWordPool = null)
        {
            List<Word> unhiddenWordsInCurrentScripture = _originalTextParts
                .Where(p => p.IsWord && !p.Word.IsHidden())
                .Select(p => p.Word)
                .ToList();

            int actualWordsToHide = Math.Min(numberToHide, unhiddenWordsInCurrentScripture.Count);

            if (actualWordsToHide == 0) return;

            List<Word> wordsSelectedForHiding = new List<Word>();

            if (globalWordPool != null && globalWordPool.Any())
            {
                // Create a temporary, shuffled copy of the global pool to pick from
                List<string> tempGlobalPool = globalWordPool.OrderBy(x => _random.Next()).ToList();

                int wordsFoundFromGlobalPool = 0;
                foreach (string globalWord in tempGlobalPool)
                {
                    if (wordsFoundFromGlobalPool >= actualWordsToHide) break;

                    // Find an unhidden instance of this global word in the current scripture
                    Word wordInstanceToHide = unhiddenWordsInCurrentScripture
                        .FirstOrDefault(w => w.GetOriginalText().Equals(globalWord, StringComparison.OrdinalIgnoreCase));

                    if (wordInstanceToHide != null)
                    {
                        wordsSelectedForHiding.Add(wordInstanceToHide);
                        // Remove this instance from the unhidden list so it's not picked again
                        unhiddenWordsInCurrentScripture.Remove(wordInstanceToHide);
                        wordsFoundFromGlobalPool++;
                    }
                }
            }

            // If we still need to hide more words after trying the global pool,
            // fill up from the remaining unhidden words in the current scripture.
            int remainingToHide = actualWordsToHide - wordsSelectedForHiding.Count;
            if (remainingToHide > 0)
            {
                // Select remaining needed words randomly from the remaining unhidden words
                // Use Except to avoid re-adding words already selected from the global pool
                var additionalWords = unhiddenWordsInCurrentScripture
                                        .Except(wordsSelectedForHiding) // Ensure no duplicates
                                        .OrderBy(x => _random.Next())
                                        .Take(remainingToHide);
                wordsSelectedForHiding.AddRange(additionalWords);
            }

            // Finally, hide all selected candidate words
            foreach (var word in wordsSelectedForHiding)
            {
                word.Hide();
            }
        }

        /// <summary>
        /// Constructs the full scripture text for display, with words hidden as appropriate.
        /// </summary>
        /// <returns>The formatted scripture text.</returns>
        public string GetDisplayText()
        {
            var displayParts = new List<string>();
            foreach (var part in _originalTextParts)
            {
                if (part.IsWord)
                {
                    displayParts.Add(part.Word.GetDisplayText());
                }
                else
                {
                    displayParts.Add(part.Delimiter);
                }
            }
            return string.Join("", displayParts);
        }

        /// <summary>
        /// Checks if all words in the scripture are currently hidden.
        /// </summary>
        /// <returns>True if all words are hidden, False otherwise.</returns>
        public bool IsCompletelyHidden()
        {
            return _originalTextParts.All(p => !p.IsWord || p.Word.IsHidden());
        }

        /// <summary>
        /// Returns the Reference object for this scripture.
        /// </summary>
        /// <returns>The Reference object.</returns>
        public Reference GetReference()
        {
            return _reference;
        }

        /// <summary>
        /// Returns a list of all Word objects in this scripture.
        /// Used by ScriptureLibrary to populate its global unique word list.
        /// </summary>
        /// <returns>A List of Word objects.</returns>
        public List<Word> GetAllWords()
        {
            return _originalTextParts.Where(p => p.IsWord).Select(p => p.Word).ToList();
        }
    }
}