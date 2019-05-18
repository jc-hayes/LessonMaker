using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using LessonsMaker.Models;

namespace LessonMaker.Data
{
    public class LessonDbContext : DbContext
    {
        private static LessonDbContext Instance;

        private LessonDbContext()
        {
            Console.WriteLine("Db Instance Created");
        }

        public static LessonDbContext GetContext()
        {
            if (Instance == null)
            {
                Instance = new LessonDbContext();
            }
            return Instance;
        }

        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=bit350;user=root;password=mypassword");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Title).IsRequired();
            });
        }
    }
}
