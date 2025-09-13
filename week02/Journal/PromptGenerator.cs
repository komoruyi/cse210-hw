// PromptGenerator.cs
using System;
using System.Collections.Generic;

public class PromptGenerator
{
    public List<string> _prompts;
    private Random _random; // To pick a random prompt

    public PromptGenerator()
    {
        _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What is one new thing I learned today?",
            "What am I grateful for today?"
        };
        _random = new Random();
    }

    public string GetRandomPrompt()
    {
        int index = _random.Next(0, _prompts.Count);
        return _prompts[index];
    }
}