using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Ticket
    {
        private Ticket()
        {

        }

        public Ticket(string subject, string details, ApplicationUser user, Indicator type, Indicator status, Tag[] tags = null, int id = 0, Indicator priority = null)
        {
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(details))
                throw new ArgumentNullException(nameof(details));
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (status == null)
                throw new ArgumentNullException(nameof(status));
            if (type.Name != IndicatorName.Type)
                throw new ArgumentException($"Object {nameof(type)} property {nameof(type.Name)} not valid.It should be 'Type' ");
            if (status.Name != IndicatorName.Status)
                throw new ArgumentException($"Object {nameof(type)} property {nameof(type.Name)} not valid.It should be 'Type' ");
            if (priority != null && priority.Name != IndicatorName.Priority)
                throw new ArgumentException($"Object {nameof(priority)} property {nameof(priority.Name)} not valid.It should be 'Type' ");
            ID = id;
            Subject = subject;
            Details = details;
            CreatedBy = user ?? throw new ArgumentNullException(nameof(user));
            UpdateIndicator(type, user);
            UpdateIndicator(status, user);
            UpdateIndicator(priority, user);
            if (tags != null)
                AddTags(tags);
        }

        public int ID { get; private set; }
        // Required
        public string Subject { get; private set; }
        // Required
        public string Details { get; private set; }
        // Required
        public string CreatedByID { get; set; }
        // Required
        [ForeignKey("CreatedByID")]
        public virtual ApplicationUser CreatedBy { get; private set; }
        // Required
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.AddHours(3);
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<Assignee> Assignees { get; private set; } = new List<Assignee>();
        public virtual ICollection<Reply> Replies { get; private set; } = new List<Reply>();
        public virtual ICollection<Tag> Tags { get; private set; } = new List<Tag>();
        public virtual ICollection<TicketIndicator> TicketIndicators { get; private set; } = new List<TicketIndicator>();
        [NotMapped]
        public TicketIndicator Priority { get { return TicketIndicators?.Where(t => t.Current && t.Indicator.Name == IndicatorName.Priority).FirstOrDefault(); } }
        [NotMapped]
        public TicketIndicator Type { get { return TicketIndicators?.Where(t => t.Current && t.Indicator.Name == IndicatorName.Type).FirstOrDefault(); } }
        [NotMapped]
        public TicketIndicator Status { get { return TicketIndicators?.Where(t => t.Current && t.Indicator.Name == IndicatorName.Status).FirstOrDefault(); } }
        [NotMapped]
        public Assignee Assignee { get { return Assignees?.Where(t => t.Current).FirstOrDefault(); } }

        private void UpdateIndicator(Indicator indicator, ApplicationUser user)
        {
            if (indicator == null)
                return;
            if (TicketIndicators.Where(ti => ti.Current && ti.Indicator.Name == indicator.Name).Any(ti => ti.Indicator.ID == indicator.ID))
            {
                return;
            }
            foreach (var tt in TicketIndicators.Where(ti => ti.Indicator.Name == indicator.Name))
            {
                tt.UnCurrent();
            }
            var newTicketIndicator = new TicketIndicator(this, user, indicator);
            TicketIndicators.Add(newTicketIndicator);
        }

        private void AddTags(Tag[] newTags)
        {
            foreach (var newTag in newTags)
            {
                if (!Tags.Any(t => t.Title == newTag.Title))
                {
                    newTag.Tickets.Add(this);
                    Tags.Add(newTag);

                }
            }
        }
        private void RemoveTag(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            if (Tags.Where(t => t.Title == tag.Title).Count() == 0)
                throw new ArgumentException(nameof(tag));
            Tags.Remove(tag);
        }
        private void UpdateTags(ICollection<Tag> newTags)
        {
            if (newTags == null)
                throw new ArgumentNullException(nameof(newTags));
            foreach (Tag tag in newTags)
            {
                if (!Tags.Contains(tag))
                {
                    Tags.Add(tag);
                }
            }
            foreach (Tag tag in Tags.Where(t => !newTags.Contains(t)))
            {
                RemoveTag(tag);
            }
        }

        public void AddReply(string content, ApplicationUser user)
        {
            Replies.Add(new Reply(content, this, user));
        }
        public void RemoveReply(Reply reply)
        {
            Replies.Remove(reply);
        }

        private void AddAssignee(Assignee assignee)
        {

            if (Assignee != null && Assignee.AssignedToID != assignee.AssignedTo.Id)
            {
                RemoveAssignee();
                Assignees.Add(assignee);
            }
            if (Assignee == null)
            {
                Assignees.Add(assignee);
            }

        }
        public void RemoveAssignee()
        {
                foreach (var a in Assignees)
                {
                    a.UnCurrent();
                }
        }
        public void UpdateAssignee(ApplicationUser newAssignTo, ApplicationUser newAssignBy)
        {
            var newAssignee = new Assignee(newAssignTo, newAssignBy, this);
            if (Assignee == null)
            {
                if (newAssignee != null)
                    AddAssignee(newAssignee);
            }
            else if (newAssignee == null)
            {
                if (Assignee != null)
                    RemoveAssignee();

            }
            else
            {
                if (Assignee.AssignedTo != newAssignee.AssignedTo)
                {
                    RemoveAssignee();
                    AddAssignee(newAssignee);
                }
            }
        }

        public void Update(Ticket updatedTicket)
        {
            if (updatedTicket.Subject != this.Subject || updatedTicket.Details != updatedTicket.Details)
            {
                this.Details = updatedTicket.Details;
                this.Subject = updatedTicket.Subject;
                this.UpdatedAt = DateTime.UtcNow.AddHours(3);
            }
            UpdateAssignee(updatedTicket.Assignee.AssignedTo, updatedTicket.Assignee.AssignedBy);
            UpdateIndicator(updatedTicket.Priority.Indicator, updatedTicket.Priority.CreatedBy);
            UpdateIndicator(updatedTicket.Type.Indicator, updatedTicket.Type.CreatedBy);
            UpdateIndicator(updatedTicket.Status.Indicator, updatedTicket.Status.CreatedBy);
            UpdateTags(updatedTicket.Tags);
        }

    }
}
