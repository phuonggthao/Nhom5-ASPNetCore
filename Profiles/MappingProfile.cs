using AutoMapper;
using backend.Models;
using backend.Dtos;

<<<<<<< HEAD
namespace backend.Profiles
=======
namespace backend.Mappings
>>>>>>> 98ae579 (upload source code)
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
<<<<<<< HEAD
            CreateMap<Class, ClassDto>()
                .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Students.Count));
            CreateMap<CreateClassDto, Class>();

            
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name));
            CreateMap<CreateStudentDto, Student>();
=======
            CreateMap<Student, StudentDto>();
            CreateMap<CreateStudentDto, Student>();
            CreateMap<Class, ClassDto>();
            CreateMap<CreateClassDto, Class>();
>>>>>>> 98ae579 (upload source code)
        }
    }
}