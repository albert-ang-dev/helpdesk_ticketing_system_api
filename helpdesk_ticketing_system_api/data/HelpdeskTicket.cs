using System.ComponentModel.DataAnnotations;

namespace helpdesk_ticketing_system_api.data
{
    public class HelpdeskTicket
    {
        [Key]
        public int ticketID { get; set; }
        public string ticketTitle { get; set; }
        public string ticketDescription { get; set; }
        public string ticketStatus { get; set; }
        public int technicianID { get; set; } // technician assigned to the ticket
        public int employeeID { get; set; } // requester

        public Employee? employee { get; set; }
        public Technician? technician { get; set; }  

    }
}
