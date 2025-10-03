using System.Collections.Generic;

namespace backend.Models
{
    public class Class
    {
        public int Id { get; set; }   // Mã lớp
        public string Name { get; set; }   // Tên lớp
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
