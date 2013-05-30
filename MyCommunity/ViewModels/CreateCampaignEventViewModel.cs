using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCommunity.ViewModels
{
    public class CreateCampaignEventViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public int CampaignID { get; set; }
    }
}