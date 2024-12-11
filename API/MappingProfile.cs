using API.Models;
using API.Models.EmployeeViewModels;
using API.Models.OfficeViewModels;
using AutoMapper;
using DAL.Models;

namespace API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleViewModel>().ReverseMap();

            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(dest => dest.OfficeTitle, opt => opt.MapFrom(src => src.Office != null ? src.Office.Name : null))
                .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => src.EmployeeRoles.Select(er => er.RoleId)))
                .ForMember(dest => dest.RoleTitles, opt => opt.MapFrom(src => src.EmployeeRoles.Select(er => er.Role.Title)));

            CreateMap<EmployeeViewModel, Employee>()
                .ForMember(dest => dest.Office, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeRoles, opt => opt.MapFrom(src => src.RoleIds.Select(roleId => new EmployeeRole { RoleId = roleId })));

            CreateMap<Office, OfficeViewModel>().ReverseMap();

            CreateMap<CreateEmployeeRequest, Employee>().ReverseMap();
            CreateMap<EditEmployeeRequest, Employee>().ReverseMap();

        }
    }
}
