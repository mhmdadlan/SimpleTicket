using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class NewReplyDto
    {
        public int? CreatedByID { get; set; }
        [Required]
        public string Content { get; set; }
    }
}