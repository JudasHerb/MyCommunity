using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(Community community )
        {
            CommunityName = community.Name;

            Campaigns = new Dictionary<int, string>();
            foreach (var campaign in community.Campaigns.Take(5))
            {
                Campaigns.Add(campaign.CampaignID, campaign.Name);
            }

            Groups = new Dictionary<int, string>();
            foreach (var group in community.Groups.Take(5))
            {
                Groups.Add(group.GroupID, group.Name);
            }
            Comments = community.Messages.ToList();
        }

        public string CommunityName { get; set; }

        public Dictionary<int, string> Campaigns { get; set; }
        public Dictionary<int, string> Groups { get; set; }
        public List<Message> Comments { get; set; }
    }
}