using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Entity;
using backend.DTOs;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ClassesController(AppDbContext context) => _context = context;

        // GET /api/classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetAllClasses()
        {
            var classes = await _context.Classes
                .Select(c => new ClassDto
                {
                    Id = c.Id,
                    Name = c.Name,                    // nếu field bên model là Name
                    StudentCount = c.Students.Count() // EF sẽ translate Count()
                })
                .ToListAsync();

            return Ok(classes);
        }

        // POST /api/classes
        [HttpPost]
        public async Task<ActionResult<ClassDto>> CreateClass([FromBody] CreateClassDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Class name is required");

            var newClass = new Class { Name = dto.Name };
            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();

            var result = new ClassDto { Id = newClass.Id, Name = newClass.Name, StudentCount = 0 };
            return CreatedAtAction(nameof(GetAllClasses), new { id = newClass.Id }, result);
        }

        // GET /api/classes/{classId}/students
        [HttpGet("{classId}/students")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentsByClass(int classId)
        {
            var exists = await _context.Classes.AnyAsync(c => c.Id == classId);
            if (!exists) return NotFound($"Class {classId} not found");

            var students = await _context.Students
                .Where(s => s.ClassId == classId)
                .Include(s => s.Class)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    DateOfBirth = s.DateOfBirth,
                    ClassName = s.Class != null ? s.Class.Name : string.Empty
                })
                .ToListAsync();

            return Ok(students);
        }
    }
}
