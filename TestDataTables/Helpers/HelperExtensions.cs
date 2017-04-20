using TestDataTables.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using TestDataTables.Models.TrackerModels;

namespace  TestDataTables.Helpers
{
    public static class HelperExtensions
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string GetName(this IIdentity user)
        {
            var ClaimsUser = (ClaimsIdentity)user;
            var claim = ClaimsUser.Claims.FirstOrDefault(c => c.Type == "Name");
            if (claim != null)
            {
                return claim.Value;
            }
            else
            {
                return null;
            }
        }

        public static string GetFullName(this IIdentity user)
        {
            var ClaimsUser = (ClaimsIdentity)user;
            var claim = (ClaimsUser).FindFirst("FullName");
            if (claim != null)
            {
                return claim.Value;
            }
            else
            {
                return null;
            }
        }

        public static ICollection<Ticket> ListTicketsForUser(this ApplicationUser user)
        {
            var projects = user.Projects.ToList();
            var tickets = new List<Ticket>();
            foreach (var p in projects)
            {
                tickets.AddRange(p.Tickets);
            }
            return (tickets);
        }

        public static bool IsUserInRole(this ApplicationUser user, string roleName)
        {
            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            return manager.IsInRole(user.Id, roleName);
        }

        public static void SendNotification(this ApplicationUser user, TicketNotification note)
        {
            Ticket ticket = db.Tickets.Find(note.TicketId);
            EmailService es = new EmailService();
            IdentityMessage message = new IdentityMessage
            {
                Destination = user.Email,
                Subject = ticket.Title,
                Body = note.Change+" : "+note.Details,
            };

            es.SendAsync(message);
        }

        public static bool Update<T>(this ApplicationDbContext db, T item, params string[] changedPropertyNames) where T : class, new()
        {

            //foreach (var prop in first.GetType().GetProperties().ToList()) { }
            //foreach (var p in typeof(Ticket).GetProperties()) {
               

            //}

            db.Set<T>().Attach(item);
            foreach (var propertyName in changedPropertyNames)
            {
                // If we can't find the property, this line will throw an exception, 
                //which is good as we want to know about it
                db.Entry(item).Property(propertyName).IsModified = true;
            }
            return true;
        }

        public static bool In(this string source, params string[] list)
        {
            if (null == source) throw new ArgumentNullException("source");
            return list.Contains(source, StringComparer.OrdinalIgnoreCase);
        }

    }
}