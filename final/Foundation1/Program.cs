using System;
using System.Collections.Generic;

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // Length in seconds
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public void DisplayVideoDetails()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}, Length: {Length} seconds, Comments: {GetNumberOfComments()}");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"Comment by {comment.Author}: {comment.Text}");
        }
        Console.WriteLine();
    }
}

class Comment
{
    public string Author { get; set; }
    public string Text { get; set; }

    public Comment(string author, string text)
    {
        Author = author;
        Text = text;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var video1 = new Video("How to Program", "Eugene Toseli", 300);
        video1.AddComment(new Comment("Alice", "Great video!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "Nice tutorial."));

        var video2 = new Video("Cooking 101", "Jane Smith", 600);
        video2.AddComment(new Comment("Dave", "Yummy recipes."));
        video2.AddComment(new Comment("Eva", "Learned a lot!"));
        video2.AddComment(new Comment("Frank", "Thanks for sharing."));

        var videos = new List<Video> { video1, video2 };

        foreach (var video in videos)
        {
            video.DisplayVideoDetails();
        }
    }
}