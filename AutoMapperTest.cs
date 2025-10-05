using AutoMapper;
using backend.Models;
using backend.Dtos;
using backend.Mappings;

namespace backend
{
    public class AutoMapperTest
    {
        public static void RunTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            var mapper = config.CreateMapper();

            var student = new Student
            {
                Id = 1,
                Name = "Phuong Thao",
                DateOfBirth = new DateTime (2005, 10, 4)
            };

            var dto = mapper.Map<StudentDto>(student);

            Console.WriteLine($" Mapping OK: {dto.Name} ({dto.DateOfBirth})");
        }
    }
}