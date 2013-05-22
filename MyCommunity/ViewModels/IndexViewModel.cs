using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(Community community, IList<Groups> groups, IList<Campaigns> campaigns)
        {
            CommunityName = community.Name;
            Groups = new PartialGroupsViewModel(groups);
            Campaigns = new PartialCampaignViewModel(campaigns);
        }

        public string CommunityName { get; set; }
        public PartialGroupsViewModel Groups { get; set; }
        public PartialCampaignViewModel Campaigns { get; set; }
    }
    
}