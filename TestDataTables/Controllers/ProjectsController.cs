using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestDataTables.Helpers;
using TestDataTables.Models;
using TestDataTables.Models.TrackerModels;

namespace TestDataTables.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            var projs = db.Projects.ToList();
            List<ProjectPMViewModel> model = new List<ProjectPMViewModel>();

            foreach (var p in projs)
            {
                ProjectPMViewModel vm = new ProjectPMViewModel();
                vm.Project = p;

                vm.ProjectManager = p.PMID != null ? db.Users.Find(p.PMID) : null;

                model.Add(vm);
            }

            return View(model);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            ProjectDetailViewModel vm = new ProjectDetailViewModel();
            //AdminProjectViewModel vm = new AdminProjectViewModel();
            UserRolesHelper helper = new UserRolesHelper();
            ProjectsHelper phelper = new ProjectsHelper();

            var pSub = phelper.ProjectUsersByRole(id.Value, "Submitter");
            var pDev = phelper.ProjectUsersByRole(id.Value, "Developer");

            var pms = helper.UsersInRole("ProjectManager");

            vm.PMUsers = new SelectList(pms, "Id", "FirstName");
            vm.TeamMembers = new SelectList(db.Users, "Id", "FullName");
            vm.Developers = pDev;
            vm.Submitters = pSub;
            vm.Project = db.Projects.Find(id);


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Project project = db.Projects.Find(id);
            if (vm.Project == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        public ActionResult AssignPM(int id)
        {
            AdminProjectViewModel vm = new AdminProjectViewModel();
            UserRolesHelper helper = new UserRolesHelper();

            var pms = helper.UsersInRole("ProjectManager");

            vm.PMUsers = new SelectList(pms, "Id", "FirstName");
            vm.Project = db.Projects.Find(id);


            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignPM(AdminProjectViewModel adminVm)
        {
            if (ModelState.IsValid)
            {
                var prj = db.Projects.Find(adminVm.Project.Id);
                prj.PMID = adminVm.SelectedUser;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(adminVm.Project.Id);
        }

        public ActionResult AddDEV(int id)
        {
            ProjectDevViewModel vm = new ProjectDevViewModel();
            UserRolesHelper helper = new UserRolesHelper();
            ProjectsHelper pHelper = new ProjectsHelper();

            var dev = helper.UsersInRole("Developer");
            var projdev = pHelper.ProjectUsersByRole(id, "Developer").Select(u => u.Id).ToArray();

            vm.DevUsers = new MultiSelectList(dev, "Id", "FirstName", projdev);
            vm.Project = db.Projects.Find(id);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDEV(ProjectDevViewModel model)
        {
            ProjectsHelper helper = new ProjectsHelper();
            if (ModelState.IsValid)
            {
                var prj = db.Projects.Find(model.Project.Id);

                foreach (var usr in prj.Users)
                {
                    helper.RemoveUserFromProject(usr.Id, prj.Id);
                }

                foreach (var dev in model.SelectedUsers)
                {
                    helper.AddUserToProject(dev, model.Project.Id);
                }

                return RedirectToAction("Details", new { id = model.Project.Id });
            }

            return View(model.Project.Id);
        }

        public PartialViewResult AddUsers(int id, List<string> SelectedUsers)
        {
            ProjectsHelper helper = new ProjectsHelper();
            var prj = db.Projects.Find(id);

            foreach (var usr in prj.Users)
            {
                helper.RemoveUserFromProject(usr.Id, id);
            }

            foreach (var dev in SelectedUsers)
            {
                helper.AddUserToProject(dev, prj.Id);
            }

            ProjectsHelper phelper = new ProjectsHelper();
            ProjectDetailViewModel model = new ProjectDetailViewModel();
            model.Developers = phelper.ProjectUsersByRole(id, "Submitter");
            model.Submitters = phelper.ProjectUsersByRole(id, "Developer");
            return PartialView("_TeamMembers", model);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Create([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
