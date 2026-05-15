using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using helpdesk_ticketing_system_api.data;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public EmployeesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Employee
    [HttpGet]
    public async Task<IActionResult> GetEmployee()
    {
        var employees = await _context.Employees.Select(e => new 
        {
            e.employeeID,
            e.firstName,
            e.lastName,
        }).ToListAsync();   

        return Ok(employees);
    }

    // GET: api/Employee/5
    [HttpGet("{employeeid}")]
    public async Task<IActionResult> GetEmployee(int employeeid)
    {
        var employees = await _context.Employees.Select(e => new
        {
            e.employeeID,
            e.firstName,
            e.lastName,
        }).FirstOrDefaultAsync(e => e.employeeID == employeeid);

        return Ok(employees);
    }

   

    // POST: api/Employee
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEmployee", new { employeeid = employee.employeeID }, employee);
    }

    // DELETE: api/Employee/5
    [HttpDelete("{employeeid}")]
    public async Task<IActionResult> DeleteEmployee(int? employeeid)
    {
        var employee = await _context.Employees.FindAsync(employeeid);
        if (employee == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EmployeeExists(int? employeeid)
    {
        return _context.Employees.Any(e => e.employeeID == employeeid);
    }
}
