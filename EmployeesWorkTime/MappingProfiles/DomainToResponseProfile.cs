using AutoMapper;
using EmployeesWorkTime.Controllers.v1.Responses;
using EmployeesWorkTime.Domain;

namespace EmployeesWorkTime.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Employee, EmployeeResponse>().ReverseMap();
        }

    }
}
