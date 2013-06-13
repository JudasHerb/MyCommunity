using System.Collections.Generic;
using System.Linq;
using MyCommunity.Models;

namespace MyCommunity.ViewModels.Community
{
    public class IndexViewModel : BaseViewModel
    {
        public Dictionary<int, string> Campaigns { get; set; }
        public Dictionary<int, string> Groups { get; set; }
        public Dictionary<int, string> Evts { get; set; }
        public int UnreadMessages { get; set; }
        public Dictionary<int, string> Neightbours { get; set; }
        public MessagesViewModel Comments { get; set; }

        public IndexViewModel With(UserProfile user)
        {
            Campaigns = new Dictionary<int, string>();
            Groups = new Dictionary<int, string>();
            Evts = new Dictionary<int, string>();
            
            Neightbours = new Dictionary<int, string>();


            foreach (UserProfile n in user.Community.Members)
            {
                if (n.UserId != user.UserId)
                {
                    if (n.DisplayName.Length > 20)
                    {
                        Neightbours.Add(n.UserId, string.Format("{0} ...", n.DisplayName.Substring(0, 20)));
                    }
                    else
                    {
                        Neightbours.Add(n.UserId, n.DisplayName);
                    }
                }
            }
            foreach (Models.Campaign campaign in user.Campaigns)
            {
                if (campaign.Name.Length > 20)
                {
                    Campaigns.Add(campaign.CampaignId, string.Format("{0} ...", campaign.Name.Substring(0, 20)));
                }
                else
                {
                    Campaigns.Add(campaign.CampaignId, campaign.Name);
                }
            }

            UnreadMessages = user.ReceivedMessages.Count(m => m.IsRead == false);


            foreach (Models.Group group in user.Groups)
            {
                if (group.Name.Length > 20)
                {
                    Groups.Add(group.GroupId, string.Format("{0} ...", group.Name.Substring(0, 20)));
                }
                else
                {
                    Groups.Add(group.GroupId, group.Name);
                }
            }

            foreach (Models.Event evt in user.Community.Events)
            {
                if (evt.Name.Length > 20)
                {
                    string summary = string.Format("{0}: {1} ...", evt.DateTime.ToShortDateString(),
                                                   evt.Name.Substring(0, 20));
                    Evts.Add(evt.EventId, summary);
                }
                else
                {
                    string summary = string.Format("{0}: {1}", evt.DateTime.ToShortDateString(), evt.Name);
                    Evts.Add(evt.EventId, summary);
                }
            }

            Comments =
                new MessagesViewModel().With(user.Community.Messages.OrderByDescending(m => m.MessageId))
                                       .Using("Community", user.CommunityID)
                                       .ShowPostForm();


            return this;
        }
    }
}