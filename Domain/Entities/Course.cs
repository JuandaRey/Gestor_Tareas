namespace TaskManagerAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;

        public ICollection<TaskItem>? Tasks { get; set; }
    }
}
