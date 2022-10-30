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
        private readonly IEmployeeService _employeeServices;

        public EmployeeController(IEmployeeService employeeServices)
        {
            _employeeServices = employeeServices;
        }

        [HttpGet(ApiRoutes.Employees.GET_ALL)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeServices.GetEmployeesAsync());
        }

        [HttpGet(ApiRoutes.Employees.GET)]
        public async Task<IActionResult> Get([FromRoute]Guid employeeId)
        {
            var employee = _employeeServices.GetEmployeeByIdAsync(employeeId);
            if (employee?.Result == null)
                return NotFound();

            return Ok(employee.Result);
        }

        [HttpPut(ApiRoutes.Employees.UPDATE)]
        public async Task<IActionResult> Update([FromRoute] Guid employeeId,[FromBody] UpdateEmployeeRequest request)
        {
            var employee = new Employee()
            {
                Id = employeeId,
                Payroll_Number = request.Payroll_Number
            };

            var update = await _employeeServices.UpdateEmployeeAsync(employee);
            if(update)
                return Ok(employee);

            return NotFound();
        }

        [HttpPost(ApiRoutes.Employees.CREATE)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest employeeRequest)
        {
            var employee = new Employee() { Payroll_Number = employeeRequest.Payroll_Number};
            

            if (employee.Id != Guid.Empty)
                employee.Id = Guid.NewGuid();

            await _employeeServices.CreateEmployeeAsync(employee);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Employees.GET.Replace("{employeeId}", employee.Id.ToString());

            var response = new EmployeeResponse() { Id = employee.Id };

            return Created(locationUri, response);
        }

        [HttpDelete(ApiRoutes.Employees.DELETE)]
        public async Task<IActionResult> Delete([FromRoute] Guid employeeId)
        {
            var deleted = await _employeeServices.DeleteEmployeeAsync(employeeId);

            if (deleted)
                return NoContent();

            return NotFound();
        }
    }
}
