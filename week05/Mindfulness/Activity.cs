public class Activity
{
    private string _name;
    private string _description;
    protected int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine($"\n{_description}");

        Console.Write("\nHow long, in seconds, would you like for your session? ");
        string input = Console.ReadLine();

        while (!int.TryParse(input, out _duration) || _duration <= 0)
        {
            Console.Write("Invalid input. Please enter a positive number for the duration: ");
            input = Console.ReadLine();
        }

        Console.Clear();
        Console.WriteLine("Get ready to begin...");
        ShowSpinner(5);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine("\nWell done!");
        ShowSpinner(3);

        Console.WriteLine($"\nYou have completed the {_name} activity for {_duration} seconds.");
        ShowSpinner(5);
    }

    protected void ShowSpinner(int seconds)
    {
        List<string> animationFrames = new List<string> { "|", "/", "-", "\\" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < endTime)
        {
            string frame = animationFrames[i % animationFrames.Count];
            Console.Write(frame);
            Thread.Sleep(250);
            Console.Write("\b");
            i++;
        }
        Console.Write(" ");
        Console.WriteLine();
    }

    protected void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);

            string spaces = new string('\b', i.ToString().Length) + new string(' ', i.ToString().Length) + new string('\b', i.ToString().Length);
            Console.Write(spaces);
        }
        Console.WriteLine();
    }

    public virtual void RunActivity()
    {
        // This is a placeholder for the specific activity logic
    }
}