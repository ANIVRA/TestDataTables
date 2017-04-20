using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestDataTables.Models.TrackerModels
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }
        public string Change { get; set; }
        public string Details { get; set; }
        public DateTimeOffset DateNotified { get; set; }

        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}