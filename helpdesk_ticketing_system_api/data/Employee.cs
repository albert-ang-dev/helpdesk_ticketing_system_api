using System.ComponentModel.DataAnnotations;

namespace helpdesk_ticketing_system_api.data
{
    public class Employee
    {
        [Key]
        public int employeeID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public List<HelpdeskTicket>? tickets { get; set; } = new();

    }


}
