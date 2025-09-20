using System;

// Assuming the Word class is defined in the same namespace or accessible via a 'using' directive.

namespace ScriptureMemorizer
{
    /// <summary>
    /// Helper class to store parts of the scripture text (words or delimiters).
    /// This helps in reconstructing the display text with correct spacing and punctuation.
    /// </summary>
    public class ScripturePart
    {
        public Word Word { get; set; } // Null if it's a delimiter
        public string Delimiter { get; set; } // Null if it's a word
        public bool IsWord { get; set; }

        public ScripturePart(Word word)
        {
            Word = word;
            IsWord = true;
        }

        public ScripturePart(string delimiter)
        {
            Delimiter = delimiter;
            IsWord = false;
        }
    }
}