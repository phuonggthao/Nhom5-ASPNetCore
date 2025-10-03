using System;

namespace backend.Models
{
    public class Student
    {
        public int Id { get; set; }   // Mã sinh viên
        public string Name { get; set; }   // Tên sinh viên
        public DateTime DateOfBirth { get; set; }   // Ngày sinh

        public int ClassId { get; set; }   // FK đến Class
        public Class? Class { get; set; }
    }
}
