using Microsoft.AspNetCore.Mvc;
using StudentTrainingPortal.Models.Data;

namespace StudentTrainingPortal.Controllers
{
    public class CustomDataController : Controller
    {
        const string route = "CustomData";
        private readonly PortalContext db;

        public CustomDataController(PortalContext context)
        {
            db = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        
        [Route(route + "/GetQuestions")]
        public IActionResult GetQuestions()
        {
            return Json(db.Questions.ToList());
        }

        [Route(route + "/GetStudents")]
        public IActionResult GetStudents()
        {
            return Json(db.Students.ToList());
        }

        [Route(route + "/GetTeachers")]
        public IActionResult GetTeachers()
        {
            return Json(db.Teachers.ToList());
        }
    }
}
