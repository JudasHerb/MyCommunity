using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyCommunity.Models
{
    public class Campaigns : IMessagable, IMermberful
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CampaignID { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<UserProfile> Members { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}