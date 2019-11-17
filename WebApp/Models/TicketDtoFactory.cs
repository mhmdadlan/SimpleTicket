using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApp.Models
{
    public class TicketDtoFactory
    {
        public static TicketDtoFactory Instance = new TicketDtoFactory();
        private TicketDtoFactory()
        {

        }

        public Ticket MapFromCreateTicketDto(CreateTicketDto createTicketDto, TicketContext db)
        {
            List<Tag> tags = new List<Tag>();
            if (createTicketDto.Tags != null)
                foreach (var tag in createTicketDto.Tags)
                {
                    var oldTag = db.Tags.Where(t => t.Title == tag).FirstOrDefault();
                    if (oldTag != null)
                    {
                        tags.Add(oldTag);
                    }
                    else
                    {
                        tags.Add(new Tag(tag));

                    }
                }
            return new Ticket(createTicketDto.Subject,
                createTicketDto.Details,
                db.Users.Where(u => u.Id == createTicketDto.CreatedByID).FirstOrDefault(),
                db.Indicators.Where(i => i.ID == createTicketDto.TypeID).FirstOrDefault(),
                db.Indicators.Where(i => i.Name == IndicatorName.Status && i.Title.ToLower() == "open").FirstOrDefault(), tags.ToArray());
        }
        public List<ListTicketsDto> MapToListTicketsDto(List<Ticket> tickets)
        {
            List<ListTicketsDto> listDto = new List<ListTicketsDto>();
            foreach (Ticket ticket in tickets)
            {
                ListTicketsDto TicketDto = new ListTicketsDto();
                TicketDto.ID = ticket.ID;
                TicketDto.Subject = ticket.Subject;
                TicketDto.CreatedAt = ticket.CreatedAt.ToString("MMM dd HH:mm");
                TicketDto.UpdatedAt = ticket.UpdatedAt.HasValue ? ticket.UpdatedAt.Value.ToString("MMM dd HH:mm") : "-";
                TicketDto.Status = ticket.Status.Indicator.Title;
                TicketDto.Type = ticket.Type.Indicator.Title;
                listDto.Add(TicketDto);
            }
            return listDto;
        }
        public ViewTicketDto MapToViewTicketDto (Ticket ticket)
        {
            ViewTicketDto viewTicketDto = new ViewTicketDto();
            viewTicketDto.ID = ticket.ID;
            viewTicketDto.Subject = ticket.Subject;
            viewTicketDto.Details = ticket.Details;
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in ticket.Tags.Select(t => t.Title).ToArray())
            {
                builder.Append(value);
                builder.Append(", ");
            }
            viewTicketDto.Tags = builder.ToString();

            viewTicketDto.Status = ticket.Status.Indicator.Title;
            viewTicketDto.Type = ticket.Type.Indicator.Title;
            viewTicketDto.CreatedAt = ticket.CreatedAt.ToString("MMM dd HH:mm");
            viewTicketDto.CreatedBy = new UserDto(ticket.CreatedBy.UserName);

            foreach(Reply reply in ticket.Replies)
            {
                viewTicketDto.Replies.Add(new ViewTicketReplyDto
                {
                    ID = reply.ID,
                    Content = reply.Content,
                    CreatedBy = new UserDto(reply.CreatedBy.UserName),
                    CreatedAt = reply.CreatedAt.ToString("MMM dd HH:mm")
                }); ;
            }
            return viewTicketDto;
        }

        public ManageTicketDashboardDto MapToManageTicketDashboardDto(Ticket ticket)
        {
            ManageTicketDashboardDto viewTicketDashboardDto = new ManageTicketDashboardDto();
            viewTicketDashboardDto.ID = ticket.ID;
            viewTicketDashboardDto.Subject = ticket.Subject;
            viewTicketDashboardDto.Details = ticket.Details;
            // Concatenate all the elements into a StringBuilder.
            StringBuilder builder = new StringBuilder();
            foreach (string value in ticket.Tags.Select(t => t.Title).ToArray())
            {
                builder.Append(value);
                builder.Append(", ");
            }
            viewTicketDashboardDto.Tags = builder.ToString();

            viewTicketDashboardDto.Status = ticket.Status.Indicator.Title;
            viewTicketDashboardDto.Type = ticket.Type.Indicator.Title;
            viewTicketDashboardDto.CreatedAt = ticket.CreatedAt.ToString("MMM dd HH:mm");
            viewTicketDashboardDto.CreatedBy = new UserDto(ticket.CreatedBy.UserName);

            foreach (Reply reply in ticket.Replies)
            {
                viewTicketDashboardDto.Replies.Add(new ViewTicketReplyDto
                {
                    ID = reply.ID,
                    Content = reply.Content,
                    CreatedBy = new UserDto(reply.CreatedBy.UserName),
                    CreatedAt = reply.CreatedAt.ToString("MMM dd HH:mm")
                }); ;
            }
            return viewTicketDashboardDto;
        }

        public List<ListTicketsDashboardDto> MapToListTicketsDashboardDto(List<Ticket> tickets)
        {
            List<ListTicketsDashboardDto> listDto = new List<ListTicketsDashboardDto>();
            foreach (Ticket ticket in tickets)
            {
                ListTicketsDashboardDto TicketDto = new ListTicketsDashboardDto();
                TicketDto.ID = ticket.ID;
                TicketDto.Subject = ticket.Subject;
                TicketDto.CreatedAt = ticket.CreatedAt.ToString("MMM dd HH:mm");
                TicketDto.UpdatedAt = ticket.UpdatedAt.HasValue ? ticket.UpdatedAt.Value.ToString("MMM dd HH:mm") : "-";
                TicketDto.Status = ticket.Status.Indicator.Title;
                TicketDto.Type = ticket.Type.Indicator.Title;
                TicketDto.Priotity = ticket.Priority?.Indicator.Title ?? "-";
                TicketDto.CreatedBy = ticket.CreatedBy.UserName;
                TicketDto.Assignee = ticket.Assignee?.AssignedTo.UserName ?? "-";
                listDto.Add(TicketDto);
            }
            return listDto;
        }

    }
}