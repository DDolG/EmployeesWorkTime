using AutoMapper;
using EmployeesWorkTime.Controllers.v1.Requests;
using EmployeesWorkTime.Domain;

namespace EmployeesWorkTime.MappingProfiles
{
    public class DomainToRequestProfile : Profile
    {
        public DomainToRequestProfile()
        {
            CreateMap<Employee, CreateEmployeeRequest>().ReverseMap();

            CreateMap<Employee, UpdateEmployeeRequest>().ReverseMap();
        }
    }
}
