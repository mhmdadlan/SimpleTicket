using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using DataAccess.Models;
using Microsoft.AspNet.Identity;

namespace WebApp.Helpers
{
    public static class Authorization
    {

        public static bool HasAccess(this Ticket ticket, IPrincipal user)
        {
            Assignee assignee = ticket.Assignee;
            string currentUserId = user.Identity.GetUserId();
            if (user.IsInRole("Admin") || user.IsInRole("Assignor"))
            {
                return true;
            }
            else if(ticket.CreatedBy.Id == currentUserId)
            {
                return true;
            }else if (assignee?.AssignedTo.Id == currentUserId)
            {
                return true;
            }
            return false;
        }
    }
}