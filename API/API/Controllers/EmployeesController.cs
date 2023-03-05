using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public EmployeesController(DatabaseContext databaseContext)
       {
            _databaseContext = databaseContext;
        }


        [HttpGet]
        // GET: async
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _databaseContext.Employee.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        // POST: async
        public async Task<IActionResult> AddEmployee([FromBody] Employee request) {
            request.Id = Guid.NewGuid();

            await _databaseContext.Employee.AddAsync(request);
            await _databaseContext.SaveChangesAsync();

            return Ok(request);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        // GET: async
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id) {
            // get an employee by id
            var employee = await _databaseContext.Employee.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null) {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        // Put: async
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee request) {
            var employee = await _databaseContext.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = request.Name;
            employee.Email = request.Email;
            employee.Phone = request.Phone;
            employee.Salary = request.Salary;
            employee.Department = request.Department;

            // update employee async
            await _databaseContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        // Delete: async
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _databaseContext.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _databaseContext.Employee.Remove(employee);
            await _databaseContext.SaveChangesAsync();

            return Ok(employee);
        }

    }
}

