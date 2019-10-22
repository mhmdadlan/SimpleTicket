using DataAccess.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class TicketTests
    {
        private Ticket ValidTicketMinimum { get; set; }
        private Ticket ValidTicketFull { get; set; }
        private Ticket ValidTicketFullWithAssignee { get; set; }

        [SetUp]
        public void SetUp()
        {
            //Arrange
            string subject = "Test";
            string details = "This is a test Test";
            ApplicationUser user = new ApplicationUser() { };
            Indicator type = new Indicator("Fix Defect", "Facing issue with software", IndicatorName.Type);
            Indicator status = new Indicator("Open", "Ticket is open and under proccessing", IndicatorName.Status);
            Indicator priority = new Indicator("Urgent", "Ticket Should be solved urgently", IndicatorName.Priority);

            Tag[] tags = new Tag[] { new Tag("tbag"), new Tag("tboo") };
            //Act
            ValidTicketMinimum = new Ticket(subject, details, user, type, status);
            ValidTicketFull = new Ticket(subject, details, user, type, status, tags, 5, priority);
            ValidTicketFullWithAssignee = new Ticket(subject, details, user, type, status, tags, 5, priority);
            ApplicationUser assignTo = new ApplicationUser() { };
            ApplicationUser assignBy = new ApplicationUser() { };
            ValidTicketFullWithAssignee.UpdateAssignee(assignTo, assignBy);
        }
        [Test]
        public void ConstructMinimumValidTicket()
        {
            //Arrange
            string subject = "Test";
            string details = "This is a test Test";
            ApplicationUser user = new ApplicationUser() { };
            Indicator type = new Indicator("Fix Defect", "Facing issue with software", IndicatorName.Type);
            Indicator status = new Indicator("Open", "Ticket is open and under proccessing", IndicatorName.Status);
            //Act
            Ticket ticket = new Ticket(subject, details, user, type, status);

            //Assert
            Assert.AreEqual(subject, ticket.Subject);
            Assert.AreEqual(details, ticket.Details);
            Assert.AreEqual(user, user);
            Assert.AreEqual(type, ticket.Type.Indicator);
            Assert.AreEqual(status, ticket.Status.Indicator);
        }
        [Test]
        public void ConstructMinimumUnValidEmptySubject()
        {
            //Arrange
            string subject = "";
            string details = "This is a test Test";
            ApplicationUser user = new ApplicationUser() { };
            Indicator type = new Indicator("Fix Defect", "Facing issue with software", IndicatorName.Type);
            Indicator status = new Indicator("Open", "Ticket is open and under proccessing", IndicatorName.Status);
            //Act
            Assert.Throws<ArgumentNullException>(() => new Ticket(subject, details, user, type, status));
        }
        [Test]
        public void ConstructMinimumUnValidEmptyDetails()
        {
            //Arrange
            string subject = "Test";
            string details = "";
            ApplicationUser user = new ApplicationUser() { };
            Indicator type = new Indicator("Fix Defect", "Facing issue with software", IndicatorName.Type);
            Indicator status = new Indicator("Open", "Ticket is open and under proccessing", IndicatorName.Status);
            //Act
            Assert.Throws<ArgumentNullException>(() => new Ticket(subject, details, user, type, status));
        }
        [Test]
        public void ConstructMinimumUnValidNullUser()
        {
            //Arrange
            string subject = "Test";
            string details = "Test Details";
            ApplicationUser user = null;
            Indicator type = new Indicator("Fix Defect", "Facing issue with software", IndicatorName.Type);
            Indicator status = new Indicator("Open", "Ticket is open and under proccessing", IndicatorName.Status);
            //Act
            Assert.Throws<ArgumentNullException>(() => new Ticket(subject, details, user, type, status));
        }
        [Test]
        public void ConstructMinimumUnValidNullType()
        {
            //Arrange
            string subject = "Test";
            string details = "Test Details";
            ApplicationUser user = new ApplicationUser() { };
            Indicator type = null;
            Indicator status = new Indicator("Open", "Ticket is open and under proccessing", IndicatorName.Status);
            //Act
            Assert.Throws<ArgumentNullException>(() => new Ticket(subject, details, user, type, status));
        }
        [Test]
        public void ConstructMinimumUnValidNullStatus()
        {
            //Arrange
            string subject = "Test";
            string details = "Test Details";
            ApplicationUser user = new ApplicationUser() { };
            Indicator type = new Indicator("Fix Defect", "Facing issue with software", IndicatorName.Type);
            Indicator status = null;
            //Act
            Assert.Throws<ArgumentNullException>(() => new Ticket(subject, details, user, type, status));
        }
        [Test]
        public void ConstructMinimumUnValidType()
        {
            //Arrange
            string subject = "Test";
            string details = "This is a test Test";
            ApplicationUser user = new ApplicationUser() { };
            Indicator type = new Indicator("Fix Defect", "Facing issue with software", IndicatorName.Status);
            Indicator status = new Indicator("Open", "Ticket is open and under proccessing", IndicatorName.Status);
            //Act
            Assert.Throws<ArgumentException>(() => new Ticket(subject, details, user, type, status));
        }
        [Test]
        public void ConstructMinimumUnValidStatus()
        {
            //Arrange
            string subject = "Test";
            string details = "This is a test Test";
            ApplicationUser user = new ApplicationUser() { };
            Indicator type = new Indicator("Fix Defect", "Facing issue with software", IndicatorName.Type);
            Indicator status = new Indicator("Open", "Ticket is open and under proccessing", IndicatorName.Priority);
            //Act
            Assert.Throws<ArgumentException>(() => new Ticket(subject, details, user, type, status));
        }


        [Test]
        public void ConstructValidTicketWithTagsIDPriority()
        {
            //Arrange
            string subject = "Test";
            string details = "This is a test Test";
            ApplicationUser user = new ApplicationUser() { };
            Indicator type = new Indicator("Fix Defect", "Facing issue with software", IndicatorName.Type);
            Indicator status = new Indicator("Open", "Ticket is open and under proccessing", IndicatorName.Status);
            Indicator priority = new Indicator("Urgent", "Ticket Should be solved urgently", IndicatorName.Priority);

            Tag[] tags = new Tag[] { new Tag("tbag"), new Tag("tboo") };
            //Act
            Ticket ticket = new Ticket(subject, details, user, type, status, tags, 5, priority);
            //Assert
            Assert.AreEqual(subject, ticket.Subject);
            Assert.AreEqual(details, ticket.Details);
            Assert.AreEqual(user, user);
            Assert.AreEqual(type, ticket.Type?.Indicator);
            Assert.AreEqual(status, ticket.Status?.Indicator);
            Assert.AreEqual(5, ticket.ID);
            Assert.AreEqual(priority, ticket.Priority?.Indicator);
            foreach (var tag in tags)
            {
                Assert.IsTrue(ticket.Tags.Any(t => t.ID == tag.ID && t.Title == tag.Title));
            }
            Assert.AreEqual(tags.Length, ticket.Tags.Count);
        }
        [Test]
        public void ConstructValidTicketWithUnValidPriority()
        {
            //Arrange
            string subject = "Test";
            string details = "This is a test Test";
            ApplicationUser user = new ApplicationUser() { };
            Indicator type = new Indicator("Fix Defect", "Facing issue with software", IndicatorName.Type);
            Indicator status = new Indicator("Open", "Ticket is open and under proccessing", IndicatorName.Status);
            Indicator priority = new Indicator("Urgent", "Ticket Should be solved urgently", IndicatorName.Status);

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => new Ticket(subject, details, user, type, status, priority: priority));

        }
        [Test]
        public void ConstructValidTicketWithEmptyTags()
        {
            //Arrange
            string subject = "Test";
            string details = "This is a test Test";
            ApplicationUser user = new ApplicationUser() { };
            Indicator type = new Indicator("Fix Defect", "Facing issue with software", IndicatorName.Type);
            Indicator status = new Indicator("Open", "Ticket is open and under proccessing", IndicatorName.Status);
            Tag[] tags = new Tag[] { };
            //Act
            Ticket ticket = new Ticket(subject, details, user, type, status, tags);
            //Assert
            Assert.AreEqual(subject, ticket.Subject);
            Assert.AreEqual(details, ticket.Details);
            Assert.AreEqual(user, user);
            Assert.AreEqual(type, ticket.Type.Indicator);
            Assert.AreEqual(status, ticket.Status.Indicator);
            foreach (var tag in tags)
            {
                Assert.IsTrue(ticket.Tags.Any(t => t.ID == tag.ID && t.Title == tag.Title));
            }
            Assert.AreEqual(tags.Length, ticket.Tags.Count);
        }


        [Test]
        public void AddFirstAssigneeValid()
        {
            ApplicationUser assignTo = new ApplicationUser() { };
            ApplicationUser assignBy = new ApplicationUser() { };
            ValidTicketMinimum.UpdateAssignee(assignTo, assignBy);
            Assert.AreEqual(assignBy, ValidTicketMinimum.Assignee.AssignedBy);
            Assert.AreEqual(assignTo, ValidTicketMinimum.Assignee.AssignedTo);
        }
        [Test]
        public void UpdateCurrentAssigneeValid()
        {
            //Arrange
            ApplicationUser assignTo = ValidTicketFullWithAssignee.Assignee.AssignedTo;
            ApplicationUser assignBy = ValidTicketFullWithAssignee.Assignee.AssignedBy;
            ApplicationUser newAssignTo = new ApplicationUser() { };
            ApplicationUser newAssignBy = new ApplicationUser() { };
            //Act

            ValidTicketFullWithAssignee.UpdateAssignee(newAssignTo, newAssignBy);
            //Assert
            Assert.AreNotEqual(assignBy, ValidTicketFullWithAssignee.Assignee.AssignedBy);
            Assert.AreNotEqual(assignTo, ValidTicketFullWithAssignee.Assignee.AssignedTo);
            Assert.AreEqual(newAssignBy, ValidTicketFullWithAssignee.Assignee.AssignedBy);
            Assert.AreEqual(newAssignTo, ValidTicketFullWithAssignee.Assignee.AssignedTo);
            Assert.That(ValidTicketFullWithAssignee.Assignees.Where(a => a.Current).Count() == 1);
            Assert.AreEqual(newAssignTo, ValidTicketFullWithAssignee.Assignees.Where(a => a.Current).First().AssignedTo);
        }
        [Test]
        public void UpdateCurrentAssigneeUnValidAssignedBy()
        {
            //Arrange
            ApplicationUser assignTo = ValidTicketFullWithAssignee.Assignee.AssignedTo;
            ApplicationUser assignBy = ValidTicketFullWithAssignee.Assignee.AssignedBy;
            ApplicationUser newAssignTo = new ApplicationUser() { };
            ApplicationUser newAssignBy = null;
            //Act

            Assert.Throws<ArgumentNullException>(() => ValidTicketFullWithAssignee.UpdateAssignee(newAssignTo, newAssignBy));
        }
        [Test]
        public void UpdateCurrentAssigneeUnValidAssignedTo()
        {
            //Arrange
            ApplicationUser assignTo = ValidTicketFullWithAssignee.Assignee.AssignedTo;
            ApplicationUser assignBy = ValidTicketFullWithAssignee.Assignee.AssignedBy;
            ApplicationUser newAssignTo = null;
            ApplicationUser newAssignBy = new ApplicationUser() { };
            //Act

            Assert.Throws<ArgumentNullException>(() => ValidTicketFullWithAssignee.UpdateAssignee(newAssignTo, newAssignBy));
        }
        [Test]
        public void RemoveCurrentAssigneeValid()
        {
            //Arrange
            //Act
            ValidTicketFullWithAssignee.RemoveAssignee();
            //Assert
            Assert.IsNull(ValidTicketFullWithAssignee.Assignee);
            Assert.IsTrue(ValidTicketFullWithAssignee.Assignees.Where(a => a.Current).Count() == 0);
            Assert.IsTrue(ValidTicketFullWithAssignee.Assignees.Count > 0);
            ValidTicketFullWithAssignee.RemoveAssignee();
            Assert.IsNull(ValidTicketFullWithAssignee.Assignee);
            Assert.IsTrue(ValidTicketFullWithAssignee.Assignees.Where(a => a.Current).Count() == 0);
            Assert.IsTrue(ValidTicketFullWithAssignee.Assignees.Count > 0);
        }


        [Test]
        public void AddReplyValid()
        {
            string content = "This is a test Ticket Description";
            ApplicationUser createdBy = new ApplicationUser();
            ValidTicketMinimum.AddReply(content, createdBy);

            Assert.IsTrue(ValidTicketMinimum.Replies.Any(r => r.Content == content));
            Assert.AreEqual(ValidTicketMinimum.Replies.Where(t => true).First().CreatedBy, createdBy);
        }
        [Test]
        public void AddReplyUnValidEmptyString()
        {
            string content = "";
            ApplicationUser createdBy = new ApplicationUser();
            Assert.Throws<ArgumentNullException>(() => ValidTicketMinimum.AddReply(content, createdBy));
        }
        [Test]
        public void AddReplyUnValidNullUser()
        {
            string content = "sdfsc";
            ApplicationUser createdBy = null;
            Assert.Throws<ArgumentNullException>(() => ValidTicketMinimum.AddReply(content, createdBy));
        }
        [Test]
        public void RemoveReplyValid()
        {
            // Arrange
            string content = "This is a test Ticket Description";
            ApplicationUser createdBy = new ApplicationUser();
            ValidTicketMinimum.AddReply(content, createdBy);
            var addedReply = ValidTicketMinimum.Replies.Where(t => true).First();
            // Act
            Assert.IsTrue(ValidTicketMinimum.Replies.Contains(addedReply));

            ValidTicketMinimum.RemoveReply(addedReply);
            Assert.IsTrue(!ValidTicketMinimum.Replies.Contains(addedReply) && ValidTicketMinimum.Replies.Count == 0);
        }
    }

}