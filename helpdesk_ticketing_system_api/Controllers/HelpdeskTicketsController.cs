using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using helpdesk_ticketing_system_api.data;

[Route("api/[controller]")]
[ApiController]
public class HelpdeskTicketsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public HelpdeskTicketsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/HelpdeskTicket
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HelpdeskTicket>>> GetHelpdeskTicket()
    {
        return await _context.Tickets.ToListAsync();
    }

    // GET: api/HelpdeskTicket/5
    [HttpGet("{ticketid}")]
    public async Task<ActionResult<HelpdeskTicket>> GetHelpdeskTicket(int ticketid)
    {
        var helpdeskticket = await _context.Tickets.FindAsync(ticketid);

        if (helpdeskticket == null)
        {
            return NotFound();
        }

        return helpdeskticket;
    }


    // get tickets of a specific employee
    [HttpGet("{empId}")]
    public async Task<ActionResult<IEnumerable<HelpdeskTicket>>> GetHelpdeskTicketByEmployee(int empId)
    {
        IEnumerable<HelpdeskTicket> helpdesktickets = await _context.Tickets.Where(t => t.employeeID == empId).ToListAsync();
        if (helpdesktickets == null)
        {
            return BadRequest();
        };

        return Ok(helpdesktickets);
    }

    // PUT: api/HelpdeskTicket/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{ticketid}")]
    public async Task<IActionResult> PutHelpdeskTicket(int? ticketid, HelpdeskTicket helpdeskticket)
    {
        if (ticketid != helpdeskticket.ticketID)
        {
            return BadRequest();
        }

        _context.Entry(helpdeskticket).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HelpdeskTicketExists(ticketid))
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

    // POST: api/HelpdeskTicket
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<HelpdeskTicket>> PostHelpdeskTicket(HelpdeskTicket helpdeskticket)
    {
        _context.Tickets.Add(helpdeskticket);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetHelpdeskTicket", new { ticketid = helpdeskticket.ticketID }, helpdeskticket);
    }

    // DELETE: api/HelpdeskTicket/5
    [HttpDelete("{ticketid}")]
    public async Task<IActionResult> DeleteHelpdeskTicket(int? ticketid)
    {
        var helpdeskticket = await _context.Tickets.FindAsync(ticketid);
        if (helpdeskticket == null)
        {
            return NotFound();
        }

        _context.Tickets.Remove(helpdeskticket);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool HelpdeskTicketExists(int? ticketid)
    {
        return _context.Tickets.Any(e => e.ticketID == ticketid);
    }
}
