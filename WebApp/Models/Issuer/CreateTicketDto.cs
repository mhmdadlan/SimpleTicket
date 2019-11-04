using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class CreateTicketDto
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public int TypeID { get; set; }
        public string[] Tags { get; set; }
        public string CreatedByID { get; set; }
        public SelectList TicketTypeSelectList { get; set; }
    }
}