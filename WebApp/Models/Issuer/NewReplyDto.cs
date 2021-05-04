﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class NewReplyDto
    {
        [Required]
        public string Content { get; set; }
        public int TicketID { get; set; } = 0;
    }
}