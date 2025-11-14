namespace TaskManagerAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Career { get; set; } = string.Empty;

        public ICollection<TaskItem>? Tasks { get; set; }
    }
}
