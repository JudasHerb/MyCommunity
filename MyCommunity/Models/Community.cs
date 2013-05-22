using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyCommunity.Models
{
    [Table("Communities")]
    public class Community : IMessagable, IMermberful
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CommunityID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<UserProfile> Members { get; set; }

        public UserProfile Admin { get; set; }

        public virtual ICollection<Campaigns> Campaigns { get; set; }

        public virtual ICollection<Groups> Groups { get; set; }

        public virtual ICollection<Events> Events { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}