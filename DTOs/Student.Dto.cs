namespace backend.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int ClassId { get; set; }
        public string ClassName { get; set; } // read-only
    }
}
