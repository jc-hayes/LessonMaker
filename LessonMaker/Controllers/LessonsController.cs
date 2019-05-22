using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LessonsMaker.Models;
using LessonMaker.Data;
using Microsoft.EntityFrameworkCore.Update;

namespace LessonsMaker.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class LessonsController : Controller
    {
        LessonDbContext _context = LessonDbContext.GetInstance();

        /// <summary>
        /// GET: api/lessons - Returns all the Lesson entities in the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet("lessons")]
        public async Task<IEnumerable<Lesson>> GetLessonsAsync()
        {
            // Return all the Lessons in the Db ordered by their Votes
            return await _context.Lessons.OrderByDescending(lesson => lesson.Votes).ToListAsync();
        }

        /// <summary>
        /// GET: api/featured-lessons - Returns all the lessons with a vote count 10 or greater.
        /// </summary>
        /// <returns></returns>
        [HttpGet("featured-lessons")]
        public async Task<IEnumerable<Lesson>> GetFeaturedLessonsAsync()
        {
            // Return all the Lessons in the Db ordered by their Votes & where votes is 10 or greater
            return await _context.Lessons.OrderByDescending(lesson => lesson.Votes).
                Where(lesson => lesson.Votes >= 10).ToListAsync();
        }

        /// <summary>
        /// This GET method returns a Lesson entity that matches the ID parameter.
        /// GET: api/lesson/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("lesson/{id}")]
        public async Task<IActionResult> GetLesson(long id)
        {
            try
            {
                // Find the lesson with the specific id
                Lesson mLesson = await _context.Lessons.FindAsync(id);
                if (mLesson == null)
                {
                    return NotFound();
                }
                return Ok(mLesson);
            }
            catch (Exception e)
            {
                return StatusCode(400, $"Exception: {e}");
            }
        }

        /// <summary>
        /// POST: api/lesson/post - Adds and saves a new Lesson entity to the database.
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        [HttpPost("lesson/post")]
        public async Task<IActionResult> PostLesson([FromBody] Lesson lesson)
        {
            lesson.CreationDate = DateTime.Today;
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLesson), new { id = lesson.ID }, lesson);
        }

        /// <summary>
        /// PUT: api/upvote/1 - Increments the vote of a lesson with {id} parameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lesson"></param>
        /// <returns></returns>
        [HttpPut("upvote/{id}")]
        public async Task<IActionResult> UpvoteLesson(long id)
        {
            Lesson mLesson = await _context.Lessons.FindAsync(id);
            if (mLesson == null)
            {
                return NotFound();
            }
            mLesson.Votes += 1;
            _context.Entry(mLesson).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// PUT: api/downvote/1 - Decrements the vote of a lesson with {id} parameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lesson"></param>
        /// <returns></returns>
        [HttpPut("downvote/{id}")]
        public async Task<IActionResult> DownvoteLeson(long id)
        {
            Lesson mLesson = await _context.Lessons.FindAsync(id);
            if (mLesson == null)
            {
                return NotFound();
            }
            if (mLesson.Votes == -2)
            {
                _context.Lessons.Remove(mLesson);
            }
            else
            {
                mLesson.Votes -= 1;
                _context.Entry(mLesson).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// PUT: api/lesson/3 - Update a lesson with {id} parameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lesson"></param>
        /// <returns></returns>
        [HttpPut("lesson/{id}")]
        public async Task<IActionResult> EditLesson(long id, [FromBody] Lesson lesson)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != lesson.ID)
                return BadRequest();
            try
            {
                var entity = await _context.Lessons.FindAsync(id);

                _context.Entry(lesson).State = EntityState.Detached;

                entity.Title = lesson.Title;
                entity.Content = lesson.Content;
                entity.Author = lesson.Author;
                
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        /// <summary>
        /// DELETE: api/delete/1 - Remove a Lesson with {id} parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteLesson(long id)
        {
            var lesson = await _context.Lessons.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}