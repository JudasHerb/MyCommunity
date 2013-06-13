using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunity.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        public UserProfile()
        {
            SentMessages = new HashSet<UserMessage>();
            ReceivedMessages = new HashSet<UserMessage>();
            Groups = new HashSet<Group>();
            Campaigns = new HashSet<Campaign>();
            Posts = new HashSet<Message>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string DisplayName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
        public bool IsCommunityAdmin { get; set; }

        //foreign key
        public int CommunityID { get; set; }

        //navigation
        public virtual Community Community { get; set; }

        public virtual ICollection<UserMessage> SentMessages { get; set; }
        public virtual ICollection<UserMessage> ReceivedMessages { get; set; }

        public virtual ICollection<Message> Posts { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}