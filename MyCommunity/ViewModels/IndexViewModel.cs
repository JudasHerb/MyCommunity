using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(UserProfile user )
        {

            

            Campaigns = new Dictionary<int, string>();
            Groups = new Dictionary<int, string>();

            if (user.Community != null)
            {
                CommunityName = user.Community.Name;
                foreach (var campaign in user.Campaigns.Take(5))
                {
                    Campaigns.Add(campaign.CampaignID, campaign.Name);
                }


                foreach (var group in user.Groups.Take(5))
                {
                    Groups.Add(group.GroupID, group.Name);
                }
                Comments = new MessagesViewModel(user.Community.Messages.OrderByDescending(m => m.MessageID).Take(5).ToList());
            }
            else
            {
                Comments = new MessagesViewModel(new List<Message>());
            }
        }

        public string CommunityName { get; set; }

        public Dictionary<int, string> Campaigns { get; set; }
        public Dictionary<int, string> Groups { get; set; }
        public MessagesViewModel Comments { get; set; }
    }

    
}