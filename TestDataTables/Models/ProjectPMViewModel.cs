using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestDataTables.Models.TrackerModels;

namespace TestDataTables.Models
{
    public class ProjectPMViewModel
    {
        public Project Project { get; set; }
        public ApplicationUser ProjectManager { get; set; }
    }
}