using EmployeesWorkTime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeesAsync();

        Task<Employee> GetEmployeeByIdAsync(Guid employerID);

        Task<bool> UpdateEmployeeAsync(Employee employerToUpdate);

        Task<bool> DeleteEmployeeAsync(Guid employerId);

        Task<bool> CreateEmployeeAsync(Employee employer);
    }
}
