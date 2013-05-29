using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class BrowseCampaignsViewModel
    {
        public BrowseCampaignsViewModel(IList<Campaigns> campaigns)
        {
            Campaigns = new Dictionary<int, string>();
            foreach (var camp in campaigns)
            {
                Campaigns.Add(camp.CampaignID, camp.Name);
            }
        }
        public Dictionary<int, string> Campaigns { get; set; }
    }
}