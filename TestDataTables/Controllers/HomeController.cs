using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestDataTables.Models;

namespace TestDataTables.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper helper = new UserRolesHelper();
        public ActionResult Index()
        {
            DashboardViewModel vm = new DashboardViewModel();
            vm.Projects = db.Projects.ToList();
            vm.Tickets = db.Tickets.ToList();
            vm.Users = db.Users.ToList();

            vm.Developers = helper.UsersInRole("Developer").ToList();
            vm.Managers = helper.UsersInRole("ProjectManager").ToList();
            vm.Submitters = helper.UsersInRole("Submitter").ToList();
            vm.Testers = helper.UsersInRole("Tester").ToList();


            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetCharts()
        {
            var resolved = db.TicketStatuses.FirstOrDefault(s => s.Name == "New");

            var statusDonut = (from status in db.TicketStatuses
                                 let tickets = db.Tickets.Where(t => t.TicketStatusId == status.Id).ToList()
                                 let ticketCount = tickets.Count()
                                 where ticketCount > 0
                                 select new
                                 {
                                     label = status.Name,
                                     value = ticketCount
                                 }).ToArray();

            //var actionDonut = (from action in db.TicketActions
            //                   let tickets = db.Tickets.Where(t => t.ActionId == action.Id).ToList()
            //                   let ticketCount = tickets.Count()
            //                   where ticketCount > 0
            //                   select new
            //                   {
            //                       label = action.Name,
            //                       value = ticketCount
            //                   }).ToArray();

            //var phaseDonut = (from phase in db.TicketPhases
            //                  let tickets = db.Tickets.Where(t => t.PhaseId == phase.Id).ToList()
            //                  let ticketCount = tickets.Count()
            //                  where ticketCount > 0
            //                  select new
            //                  {
            //                      label = phase.Name,
            //                      value = ticketCount
            //                  }).ToArray();

            var statusData = new
            {
                priorityDonut = statusDonut
                //,actionDonut = actionDonut,
                //phaseDonut = phaseDonut
            };

            return Content(JsonConvert.SerializeObject(statusData));
        }
    }
}