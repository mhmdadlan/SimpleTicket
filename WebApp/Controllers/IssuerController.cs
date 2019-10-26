using DataAccess;
using DataAccess.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Issuer")]
    public class IssuerController : Controller
    {
        protected TicketContext db = new TicketContext();
        [Route("~/")]
        [Route("Home", Name = "Home")]
        public ActionResult Home()
        {
            return View();
        }
        // GET: Ticket
        [Route("MyTickets", Name = "MyTickets")]
        public ActionResult MyTickets()
        {
            var userId = User.Identity.GetUserId();
            var ticketsList = db.Tickets.Include("TicketIndicators.Indicator").Where(t => t.CreatedByID == userId).ToList();
            List<ListTicketsDto> ticketListDto = TicketDtoFactory.Instance.MapFromTicketListToDto(ticketsList);
            return View(ticketListDto);
        }
        [Route("NewTicket", Name = "NewTicket")]
        public ActionResult NewTicket()
        {
            CreateTicketDto creatTicketVM = new CreateTicketDto();
            creatTicketVM.TicketTypeSelectList = new SelectList(db.Indicators.Where(i => i.Name == IndicatorName.Type), "ID", "Title");
            return View(creatTicketVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("NewTicket")]
        public ActionResult NewTicket(CreateTicketDto creatTicketVM)
        {
            if (!ModelState.IsValid)
            {
                creatTicketVM.TicketTypeSelectList = new SelectList(db.Indicators.Where(i => i.Name == IndicatorName.Type), "ID", "Title");
                return View(creatTicketVM);
            }
            creatTicketVM.CreatedByID = HttpContext.User.Identity.GetUserId();
            Ticket ticketDM = TicketDtoFactory.Instance.MapFromDto(creatTicketVM, db);
            db.Entry(ticketDM).State = System.Data.Entity.EntityState.Added;
            if (ticketDM.Tags != null)
                foreach (var tag in ticketDM.Tags)
                {
                    if (!(tag.ID > 0))
                        db.Entry(tag).State = System.Data.Entity.EntityState.Added;
                    else
                        db.Entry(tag).State = System.Data.Entity.EntityState.Unchanged;
                }

            db.SaveChanges();
            return RedirectToAction("MyTickets");

        }
        [Route("ViewTicket/{ID}", Name = "ViewTicket")]
        public ActionResult ViewTicket(int ID)
        {
            Ticket ticketM = db.Tickets.Include("Replies").Include("TicketIndicators.Indicator").Include("CreatedBy").Where(t => t.ID == ID).FirstOrDefault();
            if (ticketM == null)
                return HttpNotFound();
            ViewTicketDto viewTicketDto = TicketDtoFactory.Instance.MapToViewTicketDto(ticketM);
            return View(viewTicketDto);
        }
        [HttpPost]
        [Route("ViewTicket/{ID}")]
        public ActionResult ViewTicket(ViewTicketDto viewTicketDto)
        {
            if (!ModelState.IsValid)
                return RedirectToRoute("ViewTicket", new { ID = viewTicketDto.ID });
            var newReplyDto = viewTicketDto.NewReply;
            Ticket ticket = db.Tickets.Include("Replies").Where(t => t.ID == viewTicketDto.ID).FirstOrDefault();
            if (ticket == null)
                throw new ArgumentOutOfRangeException(nameof(newReplyDto));
            var userID = HttpContext.User.Identity.GetUserId();
            var user = db.Users.Where(u => u.Id == userID).FirstOrDefault();
            if (user == null)
                throw new ArgumentOutOfRangeException(nameof(newReplyDto));
            ticket.AddReply(newReplyDto.Content, user);
            db.SaveChanges();
            return RedirectToRoute("ViewTicket", new { ID = viewTicketDto.ID });
        }
    }
}