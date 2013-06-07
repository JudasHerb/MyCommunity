using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class BrowseCampaignsViewModel
    {
        public BrowseCampaignsViewModel(IList<Campaign> campaigns)
        {
            Campaigns = new List<CampaignViewModel>();
            foreach (var camp in campaigns)
            {
                Campaigns.Add(new CampaignViewModel(camp));
            }
        }
        public List<CampaignViewModel> Campaigns { get; set; }
    }
}