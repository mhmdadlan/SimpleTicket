using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class ManageTicketDashboardDto
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public ManageTicketFormDashboardDto Form { get; set; } = new ManageTicketFormDashboardDto();
        public UserDto CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public ICollection<ViewTicketReplyDto> Replies { get; set; } = new List<ViewTicketReplyDto>();
        public NewReplyDto NewReply { get; set; } = new NewReplyDto();
    }
}