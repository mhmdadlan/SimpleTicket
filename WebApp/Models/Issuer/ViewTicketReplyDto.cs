using DataAccess.Models;

namespace WebApp.Models
{
    public class ViewTicketReplyDto
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public UserDto CreatedBy { get; set; }
        public string CreatedAt { get; set; }
    }
}