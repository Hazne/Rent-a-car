using Microsoft.AspNetCore.Mvc;
using RentAPI.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentAPI.Models;
using System;

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

        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _applicationDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _applicationDbContext.Employees.AddAsync(employeeRequest);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee =  await _applicationDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Deparment = updateEmployeeRequest.Deparment;

            await _applicationDbContext.SaveChangesAsync();
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
