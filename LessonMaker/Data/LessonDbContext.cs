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
    public class LessonDbContext : DbContext
    {
        private static LessonDbContext instance;
        private static readonly String connectionString = "server=localhost;port=3306;database=bit350;user=root;password=mypassword";

        public DbSet<Lesson> Lessons { get; set; }

        private LessonDbContext()
        {
        }

        private LessonDbContext(DbContextOptions<LessonDbContext> context) :base (context)
        {
            
        }

        public static LessonDbContext GetInstance()
        {
            if (instance == null)
            {
                instance = new LessonDbContext();
            }
            return instance;
        }
        public static String GetConnectionString()
        {
            return connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(GetConnectionString()).EnableSensitiveDataLogging();
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