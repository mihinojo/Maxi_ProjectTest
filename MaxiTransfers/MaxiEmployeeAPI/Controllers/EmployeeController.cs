using MaxiEmployeeAPI.IServices;
using Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaxiEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<Response>> Get()
        {
            return new Response { IsSuccess = true, Result = await _employeeService.GetEmployees() };
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post([FromBody] Employee employee)
        {
            var emp = await _employeeService.Add(employee);
            return emp != null ? new Response { IsSuccess = true, Message = "Employee saved successfully", Result = emp } : new Response { IsSuccess = false, Message = "Error trying to add employee", Result = emp };
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Put([FromBody] Employee employee)
        {
            var emp = await _employeeService.Update(employee);
            return emp != null ? new Response { IsSuccess = true, Message = "Employee updated successfully", Result = employee } : new Response { IsSuccess = false, Message = "Error trying to update employee", Result = employee };
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            var message = await _employeeService.Delete(id);
            return string.IsNullOrEmpty(message) ? new Response { IsSuccess = true, Message = "Employee deleted successfully", Result = id } : new Response { IsSuccess = false, Message = "Error trying to delete employee", Result = id };
        }
    }
}
