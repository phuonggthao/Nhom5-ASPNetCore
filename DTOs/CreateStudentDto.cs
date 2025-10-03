namespace backend.DTOs
{
    public class CreateStudentDto
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }
    }
}
