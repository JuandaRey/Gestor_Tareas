namespace TaskManagerAPI.Dtos
{
    public class CourseResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;

        public int TotalTasks { get; set; }
        public List<int> Tasks { get; set; } = new List<int>();
    }
}
