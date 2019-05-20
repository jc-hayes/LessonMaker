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
        LessonDbContext _context = LessonDbContext.GetInstance();

        /// <summary>
        /// This GET method returns all the Lesson entities in the database and orders them by their Vote count.
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
        /// This GET method returns a Lesson entity that matches the ID parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/lesson/{id}")]
        public IActionResult Lesson(int id)
        {
            try
            {
                // Return the lesson with the specific id
                Lesson mLesson = _context.Lessons.Find(id);
                return Ok(mLesson);
            }
            catch (Exception e)
            {
                return StatusCode(400, $"Exception: {e}");
            }
        }

        /// <summary>
        /// This POST method adds and saves a new Lesson entity to the database.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="author"></param>
        /// <param name="creationDate"></param>
        /// <param name="votes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/lesson/post")]
        public IActionResult Post([FromForm] string title, [FromForm] string body, [FromForm] string author)
        {

            // Initialized the new lesson
            Lesson mLesson = new Lesson
            {
                Title = title,
                Content = body,
                Author = author,
                CreationDate = DateTime.Now,
                Votes = 0
            };
            // Add and save the new lesson to the Db
            _context.Lessons.Add(mLesson);
            _context.SaveChanges();

            return Ok(mLesson);
        }

        /// <summary>
        /// This PUT method increments the specified Lesson entity, Vote property, by 1.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/upvote/{id}")]
        public IActionResult UpVote(int id)
        {
            Lesson mLesson = _context.Lessons.Find(id);
            if (mLesson != null)
            {
                mLesson.Votes += 1;

                _context.Update(mLesson);
                _context.SaveChanges();
            }
            return Ok(mLesson);
        }

        /// <summary>
        /// This PUT method decrments the specified Lesson entity, Vote property, by 1.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/downvote/{id}")]
        public IActionResult DownVote(int id)
        {
            Lesson mLesson = _context.Lessons.Find(id);
            if (mLesson != null)
            {
                mLesson.Votes -= 1;

                _context.Update(mLesson);
                _context.SaveChanges();

            }
            return Ok(mLesson);
        }

        /// <summary>
        /// This DELETE method removes the specified Lesson entity from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Lesson mLesson = _context.Lessons.Find(id);
                _context.Lessons.Remove(mLesson);
                return Ok($"Lesson #{id} removed");
            }
            catch (Exception e)
            {
                return StatusCode(400,e);
            }
        }
    }
}