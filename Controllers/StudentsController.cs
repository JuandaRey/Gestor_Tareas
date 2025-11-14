using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using TaskManagerAPI.Models;
using TaskManagerAPI.Dtos;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResponseDto>>> GetStudents()
        {
            var students = await _context.Students
                .Include(s => s.Tasks)
                .Select(s => new StudentResponseDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Email = s.Email,
                    Career = s.Career,
                    Tasks = s.Tasks.Select(t => t.Id).ToList()
                })
                .ToListAsync();

            return Ok(students);
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponseDto>> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(s => s.Tasks)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return NotFound();

            return Ok(new StudentResponseDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                Career = student.Career,
                Tasks = student.Tasks.Select(t => t.Id).ToList()
            });
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<StudentResponseDto>> CreateStudent([FromBody] CreateStudentDto dto)
        {
            var student = new Student
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Career = dto.Career
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, new StudentResponseDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                Career = student.Career,
                Tasks = new List<int>()
            });
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto dto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            student.FullName = dto.FullName;
            student.Email = dto.Email;
            student.Career = dto.Career;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
