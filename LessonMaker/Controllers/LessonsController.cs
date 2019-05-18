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

        [HttpGet]
        [Route("api/lessons")]
        public IEnumerable<Lesson> Get()
        {
            return _context.Lessons;
        }

        [HttpGet]
        [Route("api/lesson/{id}")]
        public string Lesson(int id)
        {
            return "my lesson #" + id;
        }
    }
}