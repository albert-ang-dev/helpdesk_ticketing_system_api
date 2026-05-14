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
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
    {
        return await _context.Employees.ToListAsync();
    }

    // GET: api/Employee/5
    [HttpGet("{employeeid}")]
    public async Task<ActionResult<Employee>> GetEmployee(int employeeid)
    {
        var employee = await _context.Employees.FindAsync(employeeid);

        if (employee == null)
        {
            return NotFound();
        }

        return employee;
    }

    // PUT: api/Employee/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{employeeid}")]
    public async Task<IActionResult> PutEmployee(int? employeeid, Employee employee)
    {
        if (employeeid != employee.employeeID)
        {
            return BadRequest();
        }

        _context.Entry(employee).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeeExists(employeeid))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Employee
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        employee.employeeID = _context.Employees.Count() + 1;
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
