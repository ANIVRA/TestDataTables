using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestDataTables.Models.TrackerModels;

namespace TestDataTables.Models
{
    public class AdminProjectViewModel
    {
        public Project Project { get; set; }
        public SelectList PMUsers { get; set; }
        public string SelectedUser { get; set; }
    }

    public class ProjectDetailViewModel
    {
        public Project Project { get; set; }
        public SelectList PMUsers { get; set; }
        public string SelectedPM { get; set; }
        public List<ApplicationUser> Developers { get; set; }
        public List<ApplicationUser> Submitters { get; set; }
        public MultiSelectList TeamMembers { get; set; }
        public List<string> SelectedUsers { get; set; }
    }
}