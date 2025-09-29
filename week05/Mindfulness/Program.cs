
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Mindfulness Project.");
        //Showed Creativity by Keeping a log of how many times activities were performed.

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Activities Menu:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            Activity currentActivity = null;

            switch (choice)
            {
                case "1":
                    currentActivity = new BreathingActivity();
                    break;
                case "2":
                    currentActivity = new ReflectionActivity();
                    break;
                case "3":
                    currentActivity = new ListingActivity();
                    break;
                case "4":
                    Console.WriteLine("Exiting program. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Thread.Sleep(2000);
                    continue;
            }

            if (currentActivity != null)
            {
                currentActivity.DisplayStartingMessage();
                currentActivity.RunActivity();
                currentActivity.DisplayEndingMessage();
                Thread.Sleep(1000); // Pause before returning to the menu
            }
        }
    }
}