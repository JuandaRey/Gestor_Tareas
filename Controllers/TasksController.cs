using Microsoft.AspNetCore.Mvc;
using Infrastructure.Persistence;
using Domain.Models;


namespace Controllers
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

        // Crear una nueva tarea
        [HttpPost]
        public IActionResult CrearTarea([FromBody] TaskItem tarea)
        {
            tarea.Id = Guid.NewGuid();
            tarea.CreatedAt = DateTime.UtcNow;
            tarea.Status = "Pendiente";
            _context.Tasks.Add(tarea);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObtenerTareaPorId), new { id = tarea.Id }, tarea);
        }

        // Obtener todas las tareas
        [HttpGet]
        public IActionResult ObtenerTareas()
        {
            var tareas = _context.Tasks.ToList();
            return Ok(tareas);
        }

        // Obtener una tarea por ID
        [HttpGet("{id}")]
        public IActionResult ObtenerTareaPorId(Guid id)
        {
            var tarea = _context.Tasks.Find(id);
            if (tarea == null) return NotFound();
            return Ok(tarea);
        }

        // Actualizar una tarea
        [HttpPut("{id}")]
        public IActionResult ActualizarTarea(Guid id, [FromBody] TaskItem nuevaTarea)
        {
            var tarea = _context.Tasks.Find(id);
            if (tarea == null) return NotFound();

            tarea.Title = nuevaTarea.Title;
            tarea.Description = nuevaTarea.Description;
            tarea.Course = nuevaTarea.Course;
            tarea.DueDate = nuevaTarea.DueDate;
            tarea.Status = nuevaTarea.Status;

            _context.SaveChanges();
            return Ok(tarea);
        }

        // Eliminar una tarea
        [HttpDelete("{id}")]
        public IActionResult EliminarTarea(Guid id)
        {
            var tarea = _context.Tasks.Find(id);
            if (tarea == null) return NotFound();

            _context.Tasks.Remove(tarea);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
