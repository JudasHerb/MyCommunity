using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyCommunity.Models
{
    public class Groups
    {
        public Groups()
        {
            Members = new Collection<UserProfile>();
            Events = new Collection<Events>();
            Messages = new Collection<Message>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<UserProfile> Members { get; set; }

        public virtual ICollection<Events> Events { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}