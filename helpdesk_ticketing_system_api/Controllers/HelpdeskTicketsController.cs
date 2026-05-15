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
    public async Task<IActionResult> GetHelpdeskTicket()
    {
        var tickets = await _context.Tickets.Select(t => new
        {
            t.ticketID,
            t.ticketTitle,
            t.ticketDescription,
            t.ticketStatus,
            t.employeeID,
            t.technicianID
        }).ToListAsync();

        return Ok(tickets);
    }

    // GET: api/HelpdeskTicket/5
    [HttpGet("{ticketid}")]
    public async Task<IActionResult> GetHelpdeskTicket(int ticketid)
    {
        //var helpdeskticket = await _context.Tickets.FindAsync(ticketid);
        var tt = await _context.Tickets.Select(t => new
        {
            t.ticketID,
            t.ticketTitle,
            t.ticketDescription,
            t.ticketStatus,
            t.employeeID,
            t.technicianID
        }).FirstOrDefaultAsync(t => t.ticketID == ticketid);
        return Ok(tt);
    }


    [HttpGet("status/pending")]
    public async Task<IActionResult> GetHelpdeskTicketStatusPending()
    {
        //var helpdeskticket = await _context.Tickets.FindAsync(ticketid);
        var tt = await _context.Tickets.Where(t=> t.ticketStatus=="Pending")
            .Select(t => new
        {
            t.ticketID,
            t.ticketTitle,
            t.ticketDescription,
            t.ticketStatus,
            t.employeeID,
            t.technicianID
        }).ToListAsync();
        return Ok(tt);
    }

    [HttpGet("status/done")]
    public async Task<IActionResult> GetHelpdeskTicketStatusDone()
    {
        //var helpdeskticket = await _context.Tickets.FindAsync(ticketid);
        var tt = await _context.Tickets.Where(t => t.ticketStatus == "Done")
            .Select(t => new
            {
                t.ticketID,
                t.ticketTitle,
                t.ticketDescription,
                t.ticketStatus,
                t.employeeID,
                t.technicianID
            }).ToListAsync();
        return Ok(tt);
    }

    [HttpGet("employee/{empid}")]
    public async Task<IActionResult> GetHelpdeskTicketByEmployeeId(int empid)
    {
        //var helpdeskticket = await _context.Tickets.FindAsync(ticketid);
        var tt = await _context.Tickets.Where(t => t.employeeID == empid)
            .Select(t => new
            {
                t.ticketID,
                t.ticketTitle,
                t.ticketDescription,
                t.ticketStatus,
                t.employeeID,
                t.technicianID
            }).ToListAsync();
        return Ok(tt);
    }



    [HttpGet("technician/{tecid}")]
    public async Task<IActionResult> GetHelpdeskTicketByTechnicianId(int tecid)
    {
        //var helpdeskticket = await _context.Tickets.FindAsync(ticketid);
        var tt = await _context.Tickets.Where(t => t.technicianID == tecid)
            .Select(t => new
            {
                t.ticketID,
                t.ticketTitle,
                t.ticketDescription,
                t.ticketStatus,
                t.employeeID,
                t.technicianID
            }).ToListAsync();
        return Ok(tt);
    }


    // POST: api/HelpdeskTicket
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<HelpdeskTicket>> PostHelpdeskTicket(HelpdeskTicket helpdeskticket)
    {
        var selectedTicketToUpdate = await _context.Tickets.FirstOrDefaultAsync(t => t.ticketID == helpdeskticket.ticketID);

        if(selectedTicketToUpdate != null)
        {
            return BadRequest("There is already same ticket ID");
        };

        _context.Tickets.Add(helpdeskticket);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetHelpdeskTicket", new { ticketid = helpdeskticket.ticketID }, helpdeskticket);
    }



    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateTicketStatus(int id, [FromBody] string newStatus)
    {
        // 1. Fetch the record from PostgreSQL
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
        {
            return NotFound($"Ticket with ID {id} not found.");
        }

        // 2. Update ONLY the specific field
        ticket.ticketStatus = newStatus;

        // 3. Save changes
        // EF Core tracks the change and only sends an UPDATE for the changed column
        await _context.SaveChangesAsync();

        return Ok("Status updated successfully");
    }

    [HttpPatch("{id}/technician")]
    public async Task<IActionResult> UpdateTicketTechnician(int id, [FromBody] int newTechnician)
    {
        // 1. Fetch the record from PostgreSQL
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
        {
            return NotFound($"Ticket with ID {id} not found.");
        }

        // 2. Update ONLY the specific field
        ticket.technicianID = newTechnician;

        // 3. Save changes
        // EF Core tracks the change and only sends an UPDATE for the changed column
        await _context.SaveChangesAsync();

        return Ok("Technician updated successfully");
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

