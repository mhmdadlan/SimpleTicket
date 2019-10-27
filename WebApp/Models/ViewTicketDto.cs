using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ViewTicketDto
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public string Tags { get; set; }
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
        public string Type { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public ICollection<ViewTicketReplyDto> Replies { get; set; } = new List<ViewTicketReplyDto>();
        public NewReplyDto NewReply { get; set; } = new NewReplyDto();


    }
}