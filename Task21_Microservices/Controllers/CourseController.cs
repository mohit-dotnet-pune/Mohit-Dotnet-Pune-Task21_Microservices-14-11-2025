using CourseServices.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CourseServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        public List<Course>  courses =  new List<Course>
            {
             new() { Id = 1, Title = "Microservices Architecture", Instructor = "Dr.Allen", Credits = 4 },
             new() { Id = 2, Title = "Cloud Computing", Instructor = "Prof.Smith", Credits = 3 },
             new() { Id = 3, Title = "Data Structures", Instructor = "Dr.Lee", Credits = 4 }
             };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            return  courses.ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            return courses.FirstOrDefault(e => e.Id ==id);
        }
    }
}
