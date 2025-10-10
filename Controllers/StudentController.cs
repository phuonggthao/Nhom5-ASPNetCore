using AutoMapper;
using backend.Entity;
using backend.Dtos;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // POST /api/students
        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentDto dto)
        {
            var classExists = await _context.Classes.AnyAsync(c => c.Id == dto.ClassId);
            if (!classExists) return NotFound($"ClassId {dto.ClassId} not found");

            var student = _mapper.Map<Student>(dto);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            await _context.Entry(student).Reference(s => s.Class).LoadAsync();

            var result = _mapper.Map<StudentDto>(student);
            return CreatedAtAction(nameof(GetAllStudents), new { id = student.Id }, result);
        }

        // GET /api/students/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudentsNoPaging()
        {
            var students = await _context.Students
                .Include(s => s.Class)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Ok(result);
        }

        // PUT /api/students/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> UpdateStudent(int id, [FromBody] UpdateStudentDto dto)
        {
            var student = await _context.Students.Include(s => s.Class).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound($"Student {id} not found");

            student.Name = dto.Name;
            student.DateOfBirth = dto.DateOfBirth;

            await _context.SaveChangesAsync();

            var result = _mapper.Map<StudentDto>(student);
            return Ok(result);
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

            var students = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);

            var response = new
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = studentDtos
            };
            return Ok(response);
        }

    }
}
