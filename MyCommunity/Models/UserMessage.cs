using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunity.Models
{
    public class UserMessage
    {
        public UserMessage()
        {
            Created = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserMessageId { get; set; }

        public bool IsRead { get; set; }
        public DateTime Created { get; set; }
        public string Content { get; set; }
        public int SenderId { get; set; }
        public virtual UserProfile Sender { get; set; }
        public int AddresseeId { get; set; }
        public virtual UserProfile Addressee { get; set; }
    }
}