using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestDataTables.Models.TrackerModels;

namespace TestDataTables.Models
{
    public class ProjectDevViewModel
    {
        public Project Project { get; set; }
        public MultiSelectList DevUsers { get; set; }
        public List<string> SelectedUsers { get; set; }
    }
}