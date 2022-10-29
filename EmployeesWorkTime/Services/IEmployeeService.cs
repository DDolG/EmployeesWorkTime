using EmployeesWorkTime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployersAsync();

        Task<Employee> GetEmployerByIdAsync(Guid employerID);

        Task<bool> UpdateEmployerAsync(Employee employerToUpdate);

        Task<bool> DeleteEmployerAsync(Guid employerId);

        Task<bool> CreateEmployerAsync(Employee employer);
    }
}
