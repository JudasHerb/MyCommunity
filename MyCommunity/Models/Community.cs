using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyCommunity.Models
{
    [Table("Communities")]
    public class Community 
    {
        public Community()
        {
            Groups = new HashSet<Group>();
            Members = new HashSet<UserProfile>();
            Campaigns = new HashSet<Campaign>();
            Events = new HashSet<Event>();
            Messages = new HashSet<Message>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CommunityId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<UserProfile> Members { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Councillor> Councillors { get; set; }

        public virtual MP MP { get; set; } 
    }
}