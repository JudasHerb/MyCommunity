using System.ComponentModel.DataAnnotations;

namespace MyCommunity.ViewModels.Campaign
{
    public class CreateCampaignViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}