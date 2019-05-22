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
        public DbSet<Lesson> Lessons { get; set; }

        public LessonDbContext()
        {
        }

        private LessonDbContext(DbContextOptions<LessonDbContext> context) :base (context)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=bit350;user=root;password=mypassword").EnableSensitiveDataLogging();
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

        public static LessonDbContext GetInstance()
        {
            if (instance == null)
            {
                instance = new LessonDbContext();
            }
            return instance;
        }
    }
}