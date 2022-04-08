using AutoMapper;
using BLL.Entities;
using DAL.Entities;

namespace BLL.Utils
{
    public class AutoMappingBllDal : Profile
    {
        public AutoMappingBllDal()
        {
            CreateMap<Course, CourseDAL>().ReverseMap();

            CreateMap<Lesson, LessonDAL>().ReverseMap();
            CreateMap<Role, RoleDAL>().ReverseMap();
            CreateMap<Training, TrainingDAL>()
                .ForMember(d => d.Grade, map => map.MapFrom(s => s.Attendance ? s.Grade : Training.Mark.Failure))
                .ReverseMap();
            CreateMap<UserRole, UserRoleDAL>().ReverseMap();
            CreateMap<User, UserDAL>()
                .ForMember(d => d.FirstName, map => map.MapFrom(s => s.FirstName ?? s.Email.Substring(0, s.Email.IndexOf("@"))))
                .ReverseMap();
        }
    }
}
