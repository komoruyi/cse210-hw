using System;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Represents a single word in the scripture, with the ability to be hidden.
    /// </summary>
    public class Word
    {
        private string _text; // Private field for the original word text
        private bool _isHidden; // Private field to track hidden status

        /// <summary>
        /// Initializes a Word object.
        /// </summary>
        /// <param name="text">The original text of the word.</param>
        public Word(string text)
        {
            _text = text;
            _isHidden = false; // A word is initially not hidden
        }

        /// <summary>
        /// Hides the word.
        /// </summary>
        public void Hide()
        {
            _isHidden = true;
        }

        /// <summary>
        /// Shows the word (unhides it).
        /// </summary>
        public void Show()
        {
            _isHidden = false;
        }

        /// <summary>
        /// Checks if the word is currently hidden.
        /// </summary>
        /// <returns>True if hidden, False otherwise.</returns>
        public bool IsHidden()
        {
            return _isHidden;
        }

        /// <summary>
        /// Returns the text to display: underscores if hidden, original text otherwise.
        /// </summary>
        /// <returns>The displayable string for the word.</returns>
        public string GetDisplayText()
        {
            if (_isHidden)
            {
                // Replace alphanumeric characters with underscores, preserve punctuation.
                char[] hiddenChars = new char[_text.Length];
                for (int i = 0; i < _text.Length; i++)
                {
                    if (char.IsLetterOrDigit(_text[i]))
                    {
                        hiddenChars[i] = '_';
                    }
                    else
                    {
                        hiddenChars[i] = _text[i];
                    }
                }
                return new string(hiddenChars);
            }
            else
            {
                return _text;
            }
        }

        /// <summary>
        /// Returns the original, unhidden text of the word.
        /// </summary>
        /// <returns>The original word text.</returns>
        public string GetOriginalText()
        {
            return _text;
        }
    }
}