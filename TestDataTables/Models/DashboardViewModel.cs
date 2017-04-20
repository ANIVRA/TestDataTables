using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestDataTables.Models.TrackerModels;

namespace TestDataTables.Models
{
    public class DashboardViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public List<Project> Projects { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<ApplicationUser> Developers { get; set; }
        public List<ApplicationUser> Managers { get; set; }
        public List<ApplicationUser> Submitters { get; set; }
        public List<ApplicationUser> Testers { get; set; }

    }
}