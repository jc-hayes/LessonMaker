using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LessonsMaker.Models;
using LessonMaker.Data;

namespace LessonsMaker.Controllers
{
    [Produces("application/json")]
    //[Route("api/lesson")]
    public class LessonsController : Controller
    {
        LessonDbContext _context = LessonDbContext.GetContext();

        /// <summary>
        /// This GET method returns all the Lesson entities in the database and orders them by their Vote count
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/lessons")]
        public IEnumerable<Lesson> Get()
        { 
            // Return all the Lessons in the Db ordered by their Votes
            return _context.Lessons.OrderByDescending(lesson => lesson.Votes);
        }

        /// <summary>
        /// This GET method returns a Lesson entity that matches the ID parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/lesson/{id}")]
        public Lesson Lesson(int id)
        {
            // Return the lesson with the specific ID
            return _context.Lessons.Single(lesson=> lesson.ID == id);
        }

        /// <summary>
        /// This POST method adds and saves a new Lesson entity to the database
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="author"></param>
        /// <param name="creationDate"></param>
        /// <param name="votes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/lesson/post")]
        public IActionResult Post([FromForm] string title, [FromForm] string body, [FromForm] string author, [FromForm] DateTime creationDate, [FromForm] int votes)
        {

            // Initialized the new lesson
            Lesson mLesson = new Lesson
            {
                Title = title,
                Body = body,
                Author = author,
                CreationDate = creationDate,
                Votes = votes
            };

            // Add and save the new lesson to the Db
            _context.Lessons.Add(mLesson);
            _context.SaveChanges();

            return Ok(mLesson.ToString());
        }
    }   
}