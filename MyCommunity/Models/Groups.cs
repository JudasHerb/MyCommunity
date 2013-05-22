using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyCommunity.Models
{
    public class Groups : IMessagable, IMermberful
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupID { get; set; }
        public virtual ICollection<UserProfile> Members { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean IsPublic { get; set; }
    }
}