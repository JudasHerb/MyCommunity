using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCommunity.Models
{
    public class Councillor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CouncillorId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public int CommunityID { get; set; }
    }
}