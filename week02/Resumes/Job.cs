using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Resumes Project.");
        Job job1 = new Job();
        job1._jobTitle = "Teacher";
        job1._company = "High Flyers Academy";
        job1._startYear = 2025;
        job1._endYear = 2025;

        Job job2 = new Job();
        job2._jobTitle = "Developer";
        job2._company = "D'Laurels";
        job2._startYear = 2025;
        job2._endYear = 0;

        Resume resume = new Resume();
        resume._personName = "Ogorchukwu Lourentta";
        resume._jobs.Add(job1);
        resume._jobs.Add(job2);
        
        resume.Display();
    }
}