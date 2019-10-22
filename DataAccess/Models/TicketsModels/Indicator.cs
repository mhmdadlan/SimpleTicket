using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Indicator
    {
        Indicator()
        {

        }
        public Indicator(string title, string description, IndicatorName name)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title));
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));
            Title = title;
            Description = description;
            Name = name;
        }
        public int ID { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IndicatorName Name { get; private set; }
        public ICollection<TicketIndicator> TicketIndicators { get; private set; }
    }
}
