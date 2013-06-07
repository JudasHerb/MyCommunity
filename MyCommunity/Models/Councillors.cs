using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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