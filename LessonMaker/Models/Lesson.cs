using System;
using System.Data;
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
        public string Body { get; set; }
        public string Author { get; set; }
        public int Votes { get; set; }
        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return "New Lesson:\n\r Title=" + Title + "\n\r Body=" + Body + "\n\r Author=" + Author + "\n\r Date=" + CreationDate + "\n\r Votes=" + Votes;
        }

    }
}
