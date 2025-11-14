using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using TaskManagerAPI.Models;
using TaskManagerAPI.Dtos;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Student)
                .Include(t => t.Course)
                .Select(t => new TaskResponseDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    DueDate = t.DueDate,
                    CreatedAt = t.CreatedAt,
                    StudentId = t.StudentId,
                    CourseId = t.CourseId
                })
                .ToListAsync();

            return Ok(tasks);
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponseDto>> GetTask(int id)
        {
            var t = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (t == null)
                return NotFound();

            return Ok(new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                StudentId = t.StudentId,
                CourseId = t.CourseId
            });
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> CreateTask([FromBody] CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                DueDate = dto.DueDate,
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                StudentId = task.StudentId,
                CourseId = task.CourseId
            });
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;
            task.StudentId = dto.StudentId;
            task.CourseId = dto.CourseId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
