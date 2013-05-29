using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyCommunity.Models
{
    public class Campaigns 
    {
        public Campaigns()
        {
            Messages = new Collection<Message>();
            Members = new Collection<UserProfile>();
            Events = new Collection<Events>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CampaignID { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserProfile> Members { get; set; }
        public virtual ICollection<Events> Events { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}