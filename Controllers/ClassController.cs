using AutoMapper;
using backend.Dtos;
using backend.Models;
using backend.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClassController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetClasses()
        {
            var classes = await _context.Classes
                .Include(c => c.Students)
                .Select(c => new ClassDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    StudentCount = c.Students.Count()
                })
                .ToListAsync();

            var classDtos = _mapper.Map<IEnumerable<ClassDto>>(classes);

            return Ok(classDtos);
        }
        // POST: api/classes
        [HttpPost]
        public async Task<ActionResult<ClassDto>> CreateClass(CreateClassDto createDto)
        {
            var newClass = _mapper.Map<Class>(createDto);
            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();

            var classDto = _mapper.Map<ClassDto>(newClass);
            return CreatedAtAction(nameof(GetClasses), new { id = newClass.Id }, classDto);
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
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<StudentDto>>(students);
            return Ok(result);
        }
    }
}