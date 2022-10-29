using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesWorkTime.Controllers.v1
{
    public class UserController : Controller
    {
        [HttpGet("api/v1/user")]
        public IActionResult Get()
        {
            return Ok(new { name = "Gosha" });
        }
    }
}
