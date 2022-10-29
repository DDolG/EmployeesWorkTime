using EmployeesWorkTime.Contracts;
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
                _employees.Add(new Employer() { Id = Guid.NewGuid().ToString() });
            }
        }

        [HttpGet(ApiRoots.Employees.GET_ALL)]
        public IActionResult GetAll()
        {
            return Ok(_employees);
        }
        
        [HttpPost(ApiRoots.Employees.CREATE)]
        public IActionResult Create([FromBody] Employer employer)
        {
            if (string.IsNullOrEmpty(employer.Id))
                employer.Id = Guid.NewGuid().ToString();
            _employees.Add(employer);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoots.Employees.GET.Replace("{employerId}", employer.Id); 
            return Created(locationUri,employer);
        }

        [HttpGet("api/v1/user")]
        public IActionResult Get()
        {
            return Ok(new { name = "Gosha" });
        }
    }
}
