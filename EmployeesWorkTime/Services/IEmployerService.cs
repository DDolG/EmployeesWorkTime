using EmployeesWorkTime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Services
{
    public interface IEmployerService
    {
        List<Employer> GetEmployers();

        Employer GetEmployerById(Guid employerID);

        bool UpdateEmployer(Employer employerToUpdate);

        bool DeleteEmployer(Guid employerId);
    }
}
