using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonMaker.Models
{
    public class Lesson
    {
        private int ID { get; set; }

        private string Title { get; set; }

        private string Body { get; set; }

        private string Author { get; set; }

        private int Votes { get; set; }

        private DateTime CreationDate { get; set; }

    }
}
