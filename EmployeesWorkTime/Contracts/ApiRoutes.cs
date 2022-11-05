using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Contracts
{
    public static class ApiRoutes
    {
        public static class Employees
        {
            public const string GET_ALL = "api/v1/employees";
            public const string GET     = "api/v1/employees/{employeeId}";
            public const string UPDATE = "api/v1/employees/{employeeId}";
            public const string CREATE  = "api/v1/employees";
            public const string DELETE = "api/v1/employees/{employeeId}";
        }

        public static class UploadFile
        {
            public const string UPLOAD_FILE                         = "api/v1/upload";
            public const string CREATE_EMPLOYEE_RECORDS_IN_DATABASE = "api/v1/createEmployeesFromCsvFiles";
        }
    }
}
