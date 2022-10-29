using EmployeesWorkTime.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Controllers.v1
{
    public class UserController : Controller
    {
        private List<Employer> _employees;

        public UserController()
        {
            _employees = new List<Employer>();
            for (int i = 0; i < 5; i++)
            {
                _employees.Add(new Employer() { UserId = Guid.NewGuid().ToString() });
            }
        }

        [HttpGet("api/v1/users")]
        public IActionResult GetAll()
        {
            return Ok(_employees);
        }

        [HttpGet("api/v1/user")]
        public IActionResult Get()
        {
            return Ok(new { name = "Gosha" });
        }
    }
}
