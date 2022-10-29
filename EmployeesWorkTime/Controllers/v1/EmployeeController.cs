using EmployeesWorkTime.Contracts;
using EmployeesWorkTime.Controllers.v1.Requests;
using EmployeesWorkTime.Controllers.v1.Responses;
using EmployeesWorkTime.Domain;
using EmployeesWorkTime.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Controllers.v1
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employerServices;

        public EmployeeController(IEmployeeService employerServices)
        {
            _employerServices = employerServices;
        }

        [HttpGet(ApiRoutes.Employees.GET_ALL)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employerServices.GetEmployersAsync());
        }

        [HttpGet(ApiRoutes.Employees.GET)]
        public async Task<IActionResult> Get([FromRoute]Guid employerId)
        {
            var employer = _employerServices.GetEmployerByIdAsync(employerId);
            if (employer == null)
                return NotFound();

            return Ok(employer);
        }

        [HttpPut(ApiRoutes.Employees.UPDATE)]
        public async Task<IActionResult> Update([FromRoute] Guid employerId,[FromBody] UpdateEmployerRequest request)
        {
            var employer = new Employee()
            {
                Id = employerId,
                Payroll_Number = request.Payroll_Number
            };

            var update = await _employerServices.UpdateEmployerAsync(employer);
            if(update)
                return Ok(employer);

            return NotFound();
        }

        [HttpPost(ApiRoutes.Employees.CREATE)]
        public async Task<IActionResult> Create([FromBody] CreateEmployerRequest employerRequest)
        {
            var employer = new Employee() { Payroll_Number = employerRequest.Payroll_Number};
            

            if (employer.Id != Guid.Empty)
                employer.Id = Guid.NewGuid();

            await _employerServices.CreateEmployerAsync(employer);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Employees.GET.Replace("{employerId}", employer.Id.ToString());

            var response = new EmployeeResponse() { Id = employer.Id };

            return Created(locationUri, response);
        }

        [HttpDelete(ApiRoutes.Employees.DELETE)]
        public async Task<IActionResult> Delete([FromRoute] Guid employerId)
        {
            var deleted = await _employerServices.DeleteEmployerAsync(employerId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
