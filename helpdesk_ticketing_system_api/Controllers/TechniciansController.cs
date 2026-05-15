using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using helpdesk_ticketing_system_api.data;

[Route("api/[controller]")]
[ApiController]
public class TechniciansController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public TechniciansController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Technician
    [HttpGet]
    public async Task<IActionResult> GetTechnician()
    {
        var technicians = await _context.Technicians.Select(tc => new
        {
            tc.technicianID,
            tc.firstName,
            tc.lastName,
        }).ToListAsync();

        return Ok(technicians);
    }

    // GET: api/Technician/5
    [HttpGet("{technicianid}")]
    public async Task<IActionResult> GetTechnician(int technicianid)
    {
        var technicians = await _context.Technicians.Select(tc => new
        {
            tc.technicianID,
            tc.firstName,
            tc.lastName,
        }).ToListAsync();

        return Ok(technicians);
    }

    // PUT: api/Technician/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{technicianid}")]
    public async Task<IActionResult> PutTechnician(int? technicianid, Technician technician)
    {
        if (technicianid != technician.technicianID)
        {
            return BadRequest();
        }

        _context.Entry(technician).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TechnicianExists(technicianid))
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

    // POST: api/Technician
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Technician>> PostTechnician(Technician technician)
    {
        _context.Technicians.Add(technician);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTechnician", new { technicianid = technician.technicianID }, technician);
    }

    // DELETE: api/Technician/5
    [HttpDelete("{technicianid}")]
    public async Task<IActionResult> DeleteTechnician(int? technicianid)
    {
        var technician = await _context.Technicians.FindAsync(technicianid);
        if (technician == null)
        {
            return NotFound();
        }

        _context.Technicians.Remove(technician);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TechnicianExists(int? technicianid)
    {
        return _context.Technicians.Any(e => e.technicianID == technicianid);
    }
}
