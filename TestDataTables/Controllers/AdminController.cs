using TestDataTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestDataTables.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        UserRolesHelper helper = new UserRolesHelper();

        // GET: Admin2
        public ActionResult Index()
        {
            List<UsersViewModel> users = new List<UsersViewModel>();
            var dbUsers = db.Users.ToList();

            foreach(var usr in dbUsers)
            {
                UsersViewModel vm = new UsersViewModel();
                vm.User = usr;
                vm.Roles = helper.ListUserRoles(usr.Id).ToList();
                users.Add(vm);
            }
            return View(users);
        }

        public ActionResult AddUserRole()
        {
            ViewBag.Users = new SelectList(db.Users,"Id","FirstName");
            ViewBag.Roles = new SelectList(db.Roles, "Name", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserRole(string Users, string Roles)
        {
            helper.AddUserToRole(Users, Roles);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveUserRole(string userId)
        {
            UsersViewModel userVm = new UsersViewModel();

            userVm.User = db.Users.Find(userId);
            userVm.Roles = helper.ListUserRoles(userId).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveUserRole(string Users, string Roles)
        {
            helper.RemoveUserFromRole(Users, Roles);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var roles = db.Roles.ToList();
            var userRoles = helper.ListUserRoles(id).ToArray();

            ViewBag.User = db.Users.Find(id);
            ViewBag.Roles = new MultiSelectList(roles, "Name", "Name", userRoles);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<string> roles, string userId)
        {
            var dbRoles = db.Roles.Select(r => r.Name);

            foreach (var r in dbRoles)
            {
                helper.RemoveUserFromRole(userId, r);
            }

            if (roles != null)
            {
                foreach (var r in roles)
                {
                    helper.AddUserToRole(userId, r);
                }
            }
            return RedirectToAction("Index");
        }

    }
}