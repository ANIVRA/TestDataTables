﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestDataTables.Models.TrackerModels;

namespace TestDataTables.Models
{
    public class ProjUserViewModel
    {
        public Project Project { get; set; }
        public MultiSelectList Users { get; set; } //populates list box
        public string[] SelectedUsers { get; set; } // receives selected users

    }
    public class ProjListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsInRole { get; set; }
    }

    public class ProjUsersVM
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public IList<ApplicationUser> Users { get; set; }
        public int ticsIssued { get; set; }
        public int ticsResolved { get; set; }
    }
}