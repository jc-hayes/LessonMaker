using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using LessonMaker.Data;
using LessonsMaker.Models;
using System.Text;

namespace LessonsMaker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            InsertData();
            PrintData();
        }

        private static void InsertData()
        {
            using(var context = LessonDbContext.GetContext())
            {
                // Create the database if it does not exist
                context.Database.EnsureCreated();

                // Add some Lessons

                context.Lessons.Add(new Lesson
                {
                    Title = "My First Lesson",
                    Body = "This is the first lesson in the table.",
                    Author = "James Hayes",
                    CreationDate = DateTime.Today,
                    Votes = 4
                });
                context.Lessons.Add(new Lesson
                {
                    Title = "My Second Lesson",
                    Body = "This is the second lesson in the table.",
                    Author = "James Hayes",
                    CreationDate = DateTime.Today,
                    Votes = 10
                });
                context.Lessons.Add(new Lesson
                {
                    Title = "My third Lesson",
                    Body = "This is the third lesson in the table.",
                    Author = "James Hayes",
                    CreationDate = DateTime.Today,
                    Votes = 100
                });

                // Save changes
                context.SaveChanges();
            }
        }

        private static void PrintData()
        {
            using (var context = LessonDbContext.GetContext())
            {
                var lessons = context.Lessons;
                foreach(var lesson in lessons)
                {
                    var data = new StringBuilder();
                    data.AppendLine($"Title: {lesson.Title}");
                    data.Append($"Body: {lesson.Body}");
                    data.AppendLine($"Author: {lesson.Author}");
                    data.AppendLine($"Creation Date: {lesson.CreationDate}");
                    data.AppendLine($"Votes: {lesson.Votes}");
                    Console.WriteLine(data.ToString());
                }
            }
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
