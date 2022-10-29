using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Contracts
{
    public static class ApiRoots
    {
        public static class Employees
        {
            public const string GET_ALL = "api/v1/users";
            public const string GET     = "api/v1/users/(postId)";
        }
    }
}
