using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Entity;
using backend.DTOs;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentsController(AppDbContext context) => _context = context;

        // POST /api/students
        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentDto dto)
        {
            var classExists = await _context.Classes.AnyAsync(c => c.Id == dto.ClassId);
            if (!classExists) return NotFound($"ClassId {dto.ClassId} not found");

            var student = new Student
            {
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                ClassId = dto.ClassId
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var className = await _context.Classes
                .Where(c => c.Id == student.ClassId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync() ?? string.Empty;

            var result = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                DateOfBirth = student.DateOfBirth,
                ClassName = className
            };

            // Bạn có thể point tới GetAllStudents vì đề không yêu cầu GET /students/{id}
            return CreatedAtAction(nameof(GetAllStudents), new { id = student.Id }, result);
        }

        // GET /api/students?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult> GetAllStudents(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _context.Students.Include(s => s.Class).OrderBy(s => s.Id);

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    DateOfBirth = s.DateOfBirth,
                    ClassName = s.Class != null ? s.Class.Name : string.Empty
                })
                .ToListAsync();

            var response = new
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = items
            };

            return Ok(response);
        }

        // PUT /api/students/{id}  (KHÔNG cho đổi lớp)
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> UpdateStudent(int id, [FromBody] UpdateStudentDto dto)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound($"Student {id} not found");

            student.Name = dto.Name;
            student.DateOfBirth = dto.DateOfBirth;
            // Không thay student.ClassId

            await _context.SaveChangesAsync();

            var className = await _context.Classes
                .Where(c => c.Id == student.ClassId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync() ?? string.Empty;

            var result = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                DateOfBirth = student.DateOfBirth,
                ClassName = className
            };

            return Ok(result);
        }
    }
}
