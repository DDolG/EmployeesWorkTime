using EmployeesWorkTime.Contracts;
using EmployeesWorkTime.Controllers.v1.Requests;
using EmployeesWorkTime.Controllers.v1.Responses;
using EmployeesWorkTime.Domain;
using EmployeesWorkTime.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Controllers.v1
{
    public class UserController : Controller
    {
        private readonly IEmployerService _employerServices;

        public UserController(IEmployerService employerServices)
        {
            _employerServices = employerServices;
        }

        [HttpGet(ApiRoots.Employees.GET_ALL)]
        public IActionResult GetAll()
        {
            return Ok(_employerServices.GetEmployers());
        }

        [HttpGet(ApiRoots.Employees.GET)]
        public IActionResult Get([FromRoute]Guid employerId)
        {
            var employer = _employerServices.GetEmployerById(employerId);
            if (employer == null)
                return NotFound();

            return Ok(employer);
        }

        [HttpPost(ApiRoots.Employees.CREATE)]
        public IActionResult Create([FromBody] CreateEmployerRequest employerRequest)
        {
            var employer = new Employer() { Id = employerRequest.Id};
            

            if (employer.Id != Guid.Empty)
                employer.Id = Guid.NewGuid();
            _employerServices.GetEmployers().Add(employer);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoots.Employees.GET.Replace("{employerId}", employer.Id.ToString());

            var response = new EmployerResponse() { Id = employer.Id };

            return Created(locationUri, response);
        }
    }
}
