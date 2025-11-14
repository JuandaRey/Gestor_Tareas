namespace TaskManagerAPI.Dtos
{
    public class StudentResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Career { get; set; } = string.Empty;

        public int TotalTasks { get; set; }
        public List<int> Tasks { get; set; } = new List<int>();
    }
}
