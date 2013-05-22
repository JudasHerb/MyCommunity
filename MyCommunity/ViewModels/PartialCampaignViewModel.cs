using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class PartialCampaignViewModel
    {
        public PartialCampaignViewModel(IList<Campaigns> campaigns)
        {
            Campaigns = new Dictionary<int, string>();
            foreach (var campaign in  campaigns.Take(5))
            {
                Campaigns.Add(campaign.CampaignID, campaign.Name);
            }
        }
        public Dictionary<int, string> Campaigns { get; set; }
        
    }
}