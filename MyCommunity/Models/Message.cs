using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunity.Models
{
    public class Message
    {
        public Message()
        {
            Created = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        public DateTime Created { get; set; }

        public bool IsRead { get; set; }

        public string Content { get; set; }

        public int FromId { get; set; }
        public virtual UserProfile From { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int? CampaignId { get; set; }
        public virtual Campaign Campaign { get; set; }

        public int? EventId { get; set; }
        public virtual Event Event { get; set; }

        public int? CommunityId { get; set; }
        public virtual Community Community { get; set; }
    }
}