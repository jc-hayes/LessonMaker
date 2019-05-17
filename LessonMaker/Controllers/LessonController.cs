using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LessonMaker.Controllers
{
    [Produces("application/json")]
    [Route("api/Lesson")]
    public class LessonController : Controller
    {
    }
}