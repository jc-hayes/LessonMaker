using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.MySqlClient;
using LessonsMaker.Models;

namespace LessonMaker.Data
{
    public class LessonDbContext : DbContext, IDisposable
    {
        private static LessonDbContext instance;
        public DbSet<Lesson> Lessons { get; set; }

        private LessonDbContext()
        {
        }

        public static LessonDbContext GetContext()
        {
            if (instance == null)
            {
                instance = new LessonDbContext();
            }
            return instance;
        }


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