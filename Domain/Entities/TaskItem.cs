using System;

namespace Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }                  // Identificador único de la tarea
        public string Title { get; set; }             // Título de la tarea
        public string Description { get; set; }       // Descripción breve de la tarea
        public string Course { get; set; }            // Nombre de la materia o asignatura
        public DateTime DueDate { get; set; }         // Fecha de entrega
        public string Status { get; set; }            // Estado: Pendiente, En proceso, Completada
        public DateTime CreatedAt { get; set; }       // Fecha en que se creó la tarea
    }
}
