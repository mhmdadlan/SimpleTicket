using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class ManageTicketFormDashboardDto
    {
        public int ID { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public int StatusID { get; set; }
        public int TypeID { get; set; }
        public int? PriorityID { get; set; }
        public SelectList TypeSelectList { get; set; }
        public SelectList StatusSelectList { get; set; }
        public SelectList PrioritySelectList { get; set; }
    }
}