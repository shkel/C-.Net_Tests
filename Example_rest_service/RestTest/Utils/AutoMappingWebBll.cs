using AutoMapper;
using BLL.Entities;
using Models.Entities;
using AutoMapper.Extensions.EnumMapping;
namespace BLL.Utils
{
    public class AutoMappingWebBll : Profile
    {
        public AutoMappingWebBll()
        {
            CreateMap<AbsenteeismReport, AbsenteeismReportModel>().ReverseMap();
            CreateMap<AttendanceReport, AttendanceReportModel>().ReverseMap();
            CreateMap<Course, CourseModel>().ReverseMap();
            CreateMap<LessonModel, Lesson>().ReverseMap();
            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<Training, TrainingModel>()
                .ForMember(dest => dest.HomeWorkMark, opts => opts.MapFrom(src => src.Grade))
                .ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<UserRole, UserRoleModel>().ReverseMap();
        }
    }
}
