using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using LessonsMaker.Models;

namespace LessonMaker.Controllers
{
    [Route("[controller]/[action]")]
    public class IndexController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var webClient = new WebClient();
            var json = webClient.DownloadString("http://localhost:52000/api/lessons");
            var lessons = JsonConvert.DeserializeObject<IEnumerable<Lesson>>(json);
            
            return View(lessons);
        }
    }
}