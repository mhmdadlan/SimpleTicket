using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ListTicketsDto
    {
        public int ID { get; set; }
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Display(Name = "Issue Date")]
        public string CreatedAt { get; set; }
        [Display(Name = "Last Update Date")]
        public string UpdatedAt { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Type")]
        public string Type { get; set; }
    }
}