using AutoMapper;
using backend.Models;
using backend.Dtos;

namespace backend.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Class, ClassDto>()
                .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Students.Count));
            CreateMap<CreateClassDto, Class>();

            
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name));
            CreateMap<CreateStudentDto, Student>();
        }
    }
}