using System;
using System.Collections.Generic;

namespace VideoApp;

public class Comment
{
    public string Name { get; }
    public string Text { get; }

    public Comment(string name, string text)
    {
        Name = name ?? string.Empty;
        Text = text ?? string.Empty;
    }

    public override string ToString() => $"{Name}: {Text}";
}

public class Video
{
    public string Title { get; }
    public string Author { get; }
    public int Length { get; }

    private readonly List<Comment> _comments = new();

    public Video(string title, string author, int length)
    {
        Title = title ?? string.Empty;
        Author = author ?? string.Empty;
        Length = length < 0 ? 0 : length;
    }

    public void AddComment(Comment comment)
    {
        if (comment is null) throw new ArgumentNullException(nameof(comment));
        _comments.Add(comment);
    }

    public int NumberOfComments => _comments.Count;
    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();
}

internal static class Program
{
    private static void Main()
    {
        var video1 = new Video("How to Learn C# Programming", "Code Academy", 620);
        video1.AddComment(new Comment("John", "Great tutorial! This helped me understand C#."));
        video1.AddComment(new Comment("Sarah", "Very clear explanation. Thank you!"));
        video1.AddComment(new Comment("Michael", "I am learning C# and this video is very helpful."));

        var video2 = new Video("Build Your First Website", "Web Developer", 845);
        video2.AddComment(new Comment("David", "This is exactly what I was looking for."));
        video2.AddComment(new Comment("Anna", "The HTML explanation was easy to understand."));
        video2.AddComment(new Comment("James", "Can you make another video about CSS?"));

        var video3 = new Video("Understanding Object Oriented Programming", "Programming Master", 950);
        video3.AddComment(new Comment("Robert", "Now I understand classes and objects better."));
        video3.AddComment(new Comment("Grace", "The examples were very helpful."));
        video3.AddComment(new Comment("Daniel", "Abstraction makes much more sense now."));

        var video4 = new Video("Top 10 Programming Tips for Beginners", "Tech World", 730);
        video4.AddComment(new Comment("Chris", "These tips are really useful."));
        video4.AddComment(new Comment("Emily", "I just started programming. Thanks!"));
        video4.AddComment(new Comment("Peter", "The last tip was my favorite."));

        var videos = new List<Video> { video1, video2, video3, video4 };

        foreach (var video in videos)
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.NumberOfComments}");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Comments:");

            foreach (var comment in video.Comments)
            {
                Console.WriteLine($" - {comment}");
            }
            Console.WriteLine("========================================\n");
        }
    }
}