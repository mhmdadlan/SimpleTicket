using DataAccess;
using DataAccess.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private TicketContext _db;
        public TicketContext db
        {
            get => _db ?? HttpContext.GetOwinContext().Get<TicketContext>();
            private set
            {
                _db = value;
            }
        }
        // GET: Dashboard
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult ManageTickets(string filter)
        {
            IQueryable<Ticket> tickets = db.Tickets.Include("TicketIndicators.Indicator").Include("Assignees.AssignedTo").Include("CreatedBy");
            if (!HttpContext.User.IsInRole("Admin") && !HttpContext.User.IsInRole("Assignor"))
            {
                tickets = tickets.Where(t => t.Assignees.Where(a => a.Current && a.AssignedTo.UserName == HttpContext.User.Identity.Name).Count() == 1);
            }
            if (!string.IsNullOrEmpty(filter))
            {
                if (filter.ToLower() == "solved")
                    tickets = tickets.Where(t => t.TicketIndicators.Where(i => i.Current && i.Indicator.Name == IndicatorName.Status && i.Indicator.Title == "solved").Count() == 1);
                else if (filter.ToLower() == "unsolved")
                    tickets = tickets.Where(t => t.TicketIndicators.Where(i => i.Current && i.Indicator.Name == IndicatorName.Status && i.Indicator.Title != "solved").Count() == 1);
                else if (filter.ToLower() == "pending")
                    tickets = tickets.Where(t => t.TicketIndicators.Where(i => i.Current && i.Indicator.Name == IndicatorName.Status && i.Indicator.Title == "pending").Count() == 1);
                else if (filter.ToLower() == "unassigned")
                    tickets = tickets.Where(t => t.Assignees.Count == 0);
            }
            tickets.ToList();
            var ticketsDo = TicketDtoFactory.Instance.MapToListTicketsDashboardDto(tickets.ToList());
            return View(ticketsDo);
        }
    }
}