using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RentAPI.Helper;

namespace RentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public EmployeesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]

        public ActionResult<List<Employee>> GetAllEmployees()
        {
           var data = _applicationDbContext.Employees
                .OrderBy(x=>x.Id).AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult<Employee> AddEmployee([FromBody] EmployeeVM x)
        {
            var newEmployee = new Employee
            {
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Deparment = x.Deparment,
                Salary = x.Salary,
            };

            _applicationDbContext.Add(newEmployee);
            _applicationDbContext.SaveChanges();

            return newEmployee;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult GetEmployee([FromRoute] Guid id)
        {
            var employee = _applicationDbContext.Employees.FirstOrDefault(x => x.Id == id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult UpdateEmployee([FromRoute] Guid id, EmployeeVM updateEmployeeRequest)
        {
            var employee =  _applicationDbContext.Employees.Find(id);

            if (employee == null)
            {
                return BadRequest("Pogresan Id");
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Deparment = updateEmployeeRequest.Deparment;

            _applicationDbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _applicationDbContext.Employees.FindAsync(id);


            if (employee == null)
            {
                return NotFound();
            }

            _applicationDbContext.Employees.Remove(employee);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
