using EmployeesWorkTime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Services
{
    public class EmployerService : IEmployerService
    {
        private List<Employer> _employees;

        public EmployerService()
        {
            _employees = new List<Employer>();
            for (int i = 0; i < 5; i++)
            {
                _employees.Add(new Employer() { Id = Guid.NewGuid() });
            }
        }

        public Employer GetEmployerById(Guid employerId)
        {
            return _employees.SingleOrDefault(x => x.Id == employerId);
        }

        public List<Employer> GetEmployers()
        {
            return _employees;
        }

        public bool UpdateEmployer(Employer employerToUpdate)
        {
            var exists = GetEmployerById(employerToUpdate.Id) != null;

            if (!exists)
                return false;

            var index = _employees.FindIndex(x => x.Id == employerToUpdate.Id);
            _employees[index] = employerToUpdate;
            return true;
        }
    }
}
