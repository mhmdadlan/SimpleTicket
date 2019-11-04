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
        public string StatusColor
        {
            get
            {
                if (Status.ToLower() == "open")
                    return "info";
                else if (Status.ToLower() == "pending")
                    return "warning";
                else if (Status.ToLower() == "solved")
                    return "success";
                else
                    return "";
            }
        }
        [Display(Name = "Type")]
        public string Type { get; set; }
    }
}