namespace TaskManagerAPI.Dtos
{
    public class CreateStudentDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Career { get; set; } = string.Empty;
    }
}
