using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunity.Models
{
    public class MP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MPId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public int CommunityID { get; set; }
    }
}