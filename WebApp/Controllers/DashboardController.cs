using DataAccess;
using DataAccess.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private ApplicationUserManager _userManager;


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
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

        public ActionResult ManageTickets(string filter, string filter2 = "")
        {
            IQueryable<Ticket> tickets = db.Tickets.Include("TicketIndicators.Indicator").Include("Assignees.AssignedTo").Include("CreatedBy");
            if (HttpContext.User.IsInRole("Assignee"))
            {
                tickets = tickets.Where(t => t.Assignees.Any(a => a.Current && a.AssignedTo.UserName == HttpContext.User.Identity.Name));
            }
            if (!string.IsNullOrEmpty(filter))
            {
                if (filter.ToLower() == "mytickets")
                {
                    tickets = tickets.Where(t => t.Assignees.Where(a => a.Current && a.AssignedTo.UserName == HttpContext.User.Identity.Name).Count() == 1);
                }
                if (filter.ToLower() == "solved")
                    tickets = tickets.Where(t => t.TicketIndicators.Where(i => i.Current && i.Indicator.Name == IndicatorName.Status && i.Indicator.Title == "solved").Count() == 1);
                else if (filter.ToLower() == "unsolved" || filter2.ToLower() == "unsolved")
                    tickets = tickets.Where(t => t.TicketIndicators.Where(i => i.Current && i.Indicator.Name == IndicatorName.Status && i.Indicator.Title == "open").Count() == 1);
                else if (filter.ToLower() == "pending")
                    tickets = tickets.Where(t => t.TicketIndicators.Where(i => i.Current && i.Indicator.Name == IndicatorName.Status && i.Indicator.Title == "pending").Count() == 1);
                else if (filter.ToLower() == "unassigned")
                    tickets = tickets.Where(t => t.Assignees.Count == 0);
            }
            tickets.ToList();
            var ticketsDo = TicketDtoFactory.Instance.MapToListTicketsDashboardDto(tickets.ToList());
            return View(ticketsDo);
        }
        public ActionResult ManageTicket(int? id)
        {
            if (id == null)
                throw new ArgumentNullException();
            var ticket = db.Tickets.Include("CreatedBy").Include("Tags").Include("Assignees.AssignedTo").Include("TicketIndicators.Indicator").Include("Replies.CreatedBy").Where(t => t.ID == id).FirstOrDefault();
            if (ticket == null)
                return new HttpNotFoundResult();
            if (!ticket.HasAccess(HttpContext.User))
                return new HttpUnauthorizedResult("Unauthorized to access the requested ticket.");
            ManageTicketDashboardDto viewTicketDto = TicketDtoFactory.Instance.MapToManageTicketDashboardDto(ticket);
            return View(viewTicketDto);
        }
        [HttpPost]
        public JsonResult ReplyTicket(NewReplyDto reply)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = db.Tickets.Find(reply.TicketID) ;
                if (ticket == null)
                {
                    return Json(new { success = false, message = "Ticket Id is not valid" });
                } else if (!ticket.HasAccess(HttpContext.User))
                {
                    return Json(new { success = false, message = "Unautherized" });
                }
                try
                {
                    Reply replyModel = new Reply(reply.Content, ticket, UserManager.FindById(HttpContext.User.Identity.GetUserId()));
                    ticket.Replies.Add(replyModel);
                    db.SaveChanges();
                    var replyDto = TicketDtoFactory.Instance.MapToViewReply(replyModel);
                    return Json(new { success = true, reply = replyDto });
                } catch(Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });

                }
            }
            return Json(new { success = false, message = "You have error"});

        }
        [HttpPost]
        public JsonResult UpdateTicket(ManageTicketFormDashboardDto manageticket)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = db.Tickets.Include("Tags").Include("TicketIndicators.Indicator").Include("Assignees").Where(t => t.ID == manageticket.ID).FirstOrDefault();
                if (ticket == null)
                {
                    return Json(new { success = false, message = "Ticket Id is not valid" });
                } else if (!ticket.CanManage(HttpContext.User))
                {
                    return Json(new { success = false, message = "Unautherized" });
                }
                try
                {
                    var tags = manageticket.Tags.Where(t => !db.Tags.Any(tag => tag.Title == t)).Select(t => new Tag(t)).ToList();
                    tags.ForEach(t => t.Tickets.Add(ticket));
                    var savedTags = db.Tags.Where(tag => manageticket.Tags.Any(t => tag.Title == t)).ToList();
                    savedTags.ForEach(t => tags.Add(t));
                    ticket.UpdateTags(tags);
                    if (ticket.Status?.ID != manageticket.StatusID)
                    {
                        ticket.UpdateIndicator(db.Indicators.Where(i => i.ID == manageticket.StatusID).FirstOrDefault(), UserManager.FindById(HttpContext.User.Identity.GetUserId()));
                    }
                    if (ticket.Type?.ID != manageticket.TypeID)
                    {
                        ticket.UpdateIndicator(db.Indicators.Where(i => i.ID == manageticket.TypeID).FirstOrDefault(), UserManager.FindById(HttpContext.User.Identity.GetUserId()));
                    }
                    if (ticket.Priority?.ID != manageticket.PriorityID)
                    {
                        ticket.UpdateIndicator(db.Indicators.Where(i => i.ID == manageticket.PriorityID).FirstOrDefault(), UserManager.FindById(HttpContext.User.Identity.GetUserId()));
                    }
                    db.SaveChanges();
                    return Json(new { success = true });
                } catch(Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });

                }
            }
            return Json(new { success = false, message = "You have error"});

        }
    }
}