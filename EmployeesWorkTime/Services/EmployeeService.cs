using EmployeesWorkTime.Data;
using EmployeesWorkTime.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _dataContext;

        public EmployeeService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        
        public async Task<bool> DeleteEmployeeAsync(Guid employeeId)
        {
            var employer = await GetEmployeeByIdAsync(employeeId);
            if (employer == null)
                return false;
           _dataContext.Employers.Remove(employer);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid employeeId)
        {
            return await _dataContext.Employers.SingleOrDefaultAsync(x => x.Id == employeeId);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _dataContext.Employers.ToListAsync();
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employeeToUpdate)
        {
            _dataContext.Employers.Update(employeeToUpdate);
            var update = await _dataContext.SaveChangesAsync();
            return update > 0;
        }

        public async Task<bool> CreateEmployeeAsync(Employee employee)
        {
            await _dataContext.Employers.AddAsync(employee);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }
    }
}
