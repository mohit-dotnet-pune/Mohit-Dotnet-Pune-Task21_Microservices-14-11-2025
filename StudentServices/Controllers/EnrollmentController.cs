using Microsoft.AspNetCore.Mvc;
using StudentServices.Model;
using System.Net.Http.Json;

namespace StudentServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EnrollmentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public static List<Enrollment> Enrollments = new List<Enrollment>();

        [HttpPost]
        public async Task<ActionResult<Enrollment>> AddEnrollment([FromBody] EnrollmentDTO dto)
        {
            // 1. Retrieve Course Data from CourseService
            var client = _httpClientFactory.CreateClient("CourseService");

            var course = await client.GetFromJsonAsync<Course>(
                $"{dto.CourseId}"
            );

            if (course == null)
                return NotFound($"Course with ID {dto.CourseId} not found");

            // 2. Create Enrollment
            var enrollment = new Enrollment
            {
                Id = Enrollments.Count + 1,
                StudentId = dto.StudentId,
                StudentName = dto.StudentName,
                CourseId = course.Id,
                CourseTitle = course.Title
            };

            Enrollments.Add(enrollment);

            return Ok(enrollment);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetAllEnrollments()
        {
            return Ok(Enrollments);
        }
    }
}