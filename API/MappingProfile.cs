using AutoMapper;
using DAL.Models;
using API.Models;
using API.Models.EmployeeViewModels;
using API.Models.OfficeViewModels;

namespace API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleViewModel>().ReverseMap();

            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(dest => dest.OfficeTitle, opt => opt.MapFrom(src => src.Office != null ? src.Office.Name : null))
                .ForMember(dest => dest.RoleTitle, opt => opt.MapFrom(src => src.Role != null ? src.Role.Title : null));

            CreateMap<EmployeeViewModel, Employee>()
                .ForMember(dest => dest.Office, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<Office, OfficeViewModel>().ReverseMap();

            CreateMap<CreateEmployeeRequest, Employee>().ReverseMap();
            CreateMap<EditEmployeeRequest, Employee>().ReverseMap();

        }
    }
}
