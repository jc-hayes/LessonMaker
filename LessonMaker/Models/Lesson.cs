using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using LessonMaker.Data;

namespace LessonsMaker.Models
{
    public class Lesson
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public int Votes { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
