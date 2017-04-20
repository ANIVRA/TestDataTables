﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestDataTables.Models.TrackerModels;

namespace TestDataTables.Models
{
    public class AssignDevViewModel
    {
        public SelectList Developers { get; set; }
        public Ticket Ticket { get; set; }
        public string SelectedUser { get; set; }
    }
}