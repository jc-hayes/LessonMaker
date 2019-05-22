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
            BuildDb();
            BuildWebHost(args).Run();
        }

        private static void BuildDb()
        {
            var context = LessonDbContext.GetInstance();
            // Create the database if it does not exist
            context.Database.EnsureCreated();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}