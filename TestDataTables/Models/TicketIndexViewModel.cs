using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestDataTables.Models.TrackerModels;

namespace TestDataTables.Models
{
    public class TicketIndexViewModel
    {
        public ApplicationUser User { get; set; }
        public List<string> Roles { get; set; }
        public List<Ticket> AdminTickets { get; set; }
        public List<Ticket> PMTickets { get; set; }
        public List<Ticket> DevTickets { get; set; }
        public List<Ticket> SubTickets { get; set; }
    }
}