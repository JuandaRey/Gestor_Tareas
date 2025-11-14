using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using TaskManagerAPI.Models;
using TaskManagerAPI.Dtos;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseResponseDto>>> GetCourses()
        {
            var courses = await _context.Courses
                .Include(c => c.Tasks)
                .Select(c => new CourseResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Teacher = c.Teacher,
                    Tasks = c.Tasks.Select(t => t.Id).ToList()
                })
                .ToListAsync();

            return Ok(courses);
        }

        // GET: api/courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseResponseDto>> GetCourse(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Tasks)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound();

            return Ok(new CourseResponseDto
            {
                Id = course.Id,
                Name = course.Name,
                Teacher = course.Teacher,
                Tasks = course.Tasks.Select(t => t.Id).ToList()
            });
        }

        // POST: api/courses
        [HttpPost]
        public async Task<ActionResult<CourseResponseDto>> CreateCourse([FromBody] CreateCourseDto dto)
        {
            var course = new Course
            {
                Name = dto.Name,
                Teacher = dto.Teacher
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, new CourseResponseDto
            {
                Id = course.Id,
                Name = course.Name,
                Teacher = course.Teacher,
                Tasks = new List<int>()
            });
        }

        // PUT: api/courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto dto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            course.Name = dto.Name;
            course.Teacher = dto.Teacher;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
