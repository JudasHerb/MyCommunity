using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunity.Models
{
    public class Event
    {
        public Event()
        {
            Messages = new HashSet<Message>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }

        public int? CampaignId { get; set; }
        public virtual Campaign Campaign { get; set; }

        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int? CommunityId { get; set; }
        public virtual Community Community { get; set; }
    }
}