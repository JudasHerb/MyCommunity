using System;
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
            Evts = new Dictionary<int, string>();
            Messages = new Dictionary<int, string>();
            if (user.Community != null)
            {
                CommunityName = user.Community.Name;
                foreach (var campaign in user.Campaigns.Take(5))
                {
                    if (campaign.Name.Length > 10)
                    {
                        Campaigns.Add(campaign.CampaignID, string.Format("{0} ...", campaign.Name.Substring(0, 10)));
                    }
                    else
                    {
                        Campaigns.Add(campaign.CampaignID, campaign.Name);
                    }
                    
                }
                foreach (var msg in user.Messages.Take(5))
                {

                    var msgContent = msg.Content ?? "Empty";
                    if (msgContent.Length > 10)
                    {
                        var summary = string.Format("{0}: {1} ...", msg.From.UserName, msgContent.Substring(0,10));
                        Messages.Add(msg.MessageID, summary);
                    }
                    else
                    {
                        var summary = string.Format("{0}: {1}", msg.From.UserName, msgContent);
                        Messages.Add(msg.MessageID, summary);
                    }
                    
                }

                foreach (var group in user.Groups.Take(5))
                {
                    if (group.Name.Length > 10)
                    {
                        Groups.Add(group.GroupID, string.Format("{0} ...", group.Name.Substring(0, 10)));
                    }
                    else
                    {
                        Groups.Add(group.GroupID,  group.Name);
                    }
                    
                }

                foreach (var evt in user.Community.Events)
                {
                    if (evt.Name.Length > 10)
                    {
                        var summary = string.Format("{0}: {1} ...", evt.DateTime.ToShortDateString(), evt.Name.Substring(0, 10));
                        Evts.Add(evt.EventID, summary);    
                    }
                    else
                    {
                        var summary = string.Format("{0}: {1}" , evt.DateTime.ToShortDateString(), evt.Name);
                        Evts.Add(evt.EventID, summary);    
                    }
                    
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
        public Dictionary<int, string> Evts { get; set; }
        public Dictionary<int, string> Messages { get; set; }

        public MessagesViewModel Comments { get; set; }
}
    }

    
