public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private Random _random = new Random();

    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public override void RunActivity()
    {
        Console.WriteLine("\nConsider the following prompt:");
        string prompt = _prompts[_random.Next(_prompts.Count)];
        Console.WriteLine($"--- {prompt} ---");

        Console.WriteLine("You may begin in: ");
        ShowCountDown(5);

        Console.WriteLine("\nStart listing items:");
        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        int itemCount = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string item = Console.ReadLine();
            // This is a simple way to count items.
            // A more advanced solution would be needed for a precise time limit on input.
            if (!string.IsNullOrWhiteSpace(item))
            {
                itemCount++;
            }
            if (DateTime.Now >= endTime)
            {
                break;
            }
        }

        Console.WriteLine($"\nYou listed {itemCount} items.");
    }
}