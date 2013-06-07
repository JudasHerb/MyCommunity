using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyCommunity.Models
{
    public class Campaign 
    {
        public Campaign()
        {
            Messages = new HashSet<Message>();
            Members = new HashSet<UserProfile>();
            Events = new HashSet<Event>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CampaignId { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserProfile> Members { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CommunityID { get; set; }
        public virtual Community Community { get; set; }
    }
}