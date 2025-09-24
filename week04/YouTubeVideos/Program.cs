using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    // Comment class represents a comment on a video
    public class Comment
    {
        public string CommenterName { get; set; }
        public string Text { get; set; }

        public Comment(string commenterName, string text)
        {
            CommenterName = commenterName;
            Text = text;
        }
    }

    // Video class represents a YouTube video and its comments
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int LengthSeconds { get; set; }
        private List<Comment> comments = new List<Comment>();

        public Video(string title, string author, int lengthSeconds)
        {
            Title = title;
            Author = author;
            LengthSeconds = lengthSeconds;
        }

        public void AddComment(Comment comment)
        {
            comments.Add(comment);
        }

        public int GetNumberOfComments()
        {
            return comments.Count;
        }

        public List<Comment> GetComments()
        {
            return comments;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create videos and add comments
            var videos = new List<Video>();

            var video1 = new Video("How to Bake Bread", "Chef Emma", 300);
            video1.AddComment(new Comment("Alice", "Great tutorial!"));
            video1.AddComment(new Comment("Bob", "I tried this recipe, and it was awesome."));
            video1.AddComment(new Comment("Sarah", "Can you do a gluten-free version?"));
            videos.Add(video1);

            var video2 = new Video("Simple Yoga Routine", "YogaWithAlex", 600);
            video2.AddComment(new Comment("Tom", "Very relaxing."));
            video2.AddComment(new Comment("Jane", "Helped me start my day!"));
            video2.AddComment(new Comment("Mike", "Could you do a longer routine next time?"));
            videos.Add(video2);

            var video3 = new Video("DIY Desk Organizer", "CraftySam", 450);
            video3.AddComment(new Comment("Lucy", "Super useful!"));
            video3.AddComment(new Comment("David", "Loved the step-by-step instructions."));
            video3.AddComment(new Comment("Anna", "Can you show more recycling crafts?"));
            videos.Add(video3);

            var video4 = new Video("Travel Vlog: Paris", "WanderLust", 900);
            video4.AddComment(new Comment("Chris", "Awesome footage!"));
            video4.AddComment(new Comment("Sophie", "Paris looks amazing."));
            video4.AddComment(new Comment("Jake", "Thanks for the travel tips."));
            videos.Add(video4);

            // Display information about each video and its comments
            foreach (var video in videos)
            {
                Console.WriteLine($"Title: {video.Title}");
                Console.WriteLine($"Author: {video.Author}");
                Console.WriteLine($"Length (seconds): {video.LengthSeconds}");
                Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
                Console.WriteLine("Comments:");
                foreach (var comment in video.GetComments())
                {
                    Console.WriteLine($" - {comment.CommenterName}: {comment.Text}");
                }
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}