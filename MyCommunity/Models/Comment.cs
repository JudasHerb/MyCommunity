using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunity.Models
{
    public class Comment
    {
        public Comment()
        {
            Created = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        public string Text { get; set; }

        public DateTime Created { get; set; }

        public int FromId { get; set; }
        public virtual UserProfile From { get; set; }

        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
    }
}