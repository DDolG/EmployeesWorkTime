using AutoMapper;
using EmployeesWorkTime.Controllers.v1.Requests;
using EmployeesWorkTime.Domain;

namespace EmployeesWorkTime.MappingProfiles
{
    public class DomainToUploadCsvProfile : Profile
    {
        public DomainToUploadCsvProfile()
        {
            CreateMap<Employee, EmployeeCsvRecord>().ReverseMap();
        }

    }
}
