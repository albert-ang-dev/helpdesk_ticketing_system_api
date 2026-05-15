using System.ComponentModel.DataAnnotations;

namespace helpdesk_ticketing_system_api.data
{
    public class Technician
    {
        [Key]
        public int technicianID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public List<HelpdeskTicket>? tickets { get; set; } = new();

    }
}
