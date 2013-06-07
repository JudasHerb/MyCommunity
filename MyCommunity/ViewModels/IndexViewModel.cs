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
            Neightbours = new Dictionary<int, string>();
            if (user.Community != null)
            {
                CommunityName = user.Community.Name;
                
                foreach (var n in user.Community.Members.Take(5))
                {
                    if (n.UserId!=user.UserId)
                    {
                        if (n.DisplayName.Length > 10)
                        {
                            Neightbours.Add(n.UserId, string.Format("{0} ...", n.DisplayName.Substring(0, 10)));
                        }
                        else
                        {
                            Neightbours.Add(n.UserId, n.DisplayName);
                        }
                    }
                }
                foreach (var campaign in user.Campaigns.Take(5))
                {
                    if (campaign.Name.Length > 10)
                    {
                        Campaigns.Add(campaign.CampaignId, string.Format("{0} ...", campaign.Name.Substring(0, 10)));
                    }
                    else
                    {
                        Campaigns.Add(campaign.CampaignId, campaign.Name);
                    }
                    
                }
                foreach (var msg in user.Messages.Take(5))
                {

                    var msgContent = msg.Content ?? "Empty";
                    if (msgContent.Length > 10)
                    {
                        var summary = string.Format("{0}: {1} ...", msg.From, msgContent.Substring(0,10));
                        Messages.Add(msg.MessageId, summary);
                    }
                    else
                    {
                        var summary = string.Format("{0}: {1}", msg.From, msgContent);
                        Messages.Add(msg.MessageId, summary);
                    }
                    
                }

                foreach (var group in user.Groups.Take(5))
                {
                    if (group.Name.Length > 10)
                    {
                        Groups.Add(group.GroupId, string.Format("{0} ...", group.Name.Substring(0, 10)));
                    }
                    else
                    {
                        Groups.Add(group.GroupId, group.Name);
                    }
                    
                }

                foreach (var evt in user.Community.Events)
                {
                    if (evt.Name.Length > 10)
                    {
                        var summary = string.Format("{0}: {1} ...", evt.DateTime.ToShortDateString(), evt.Name.Substring(0, 10));
                        Evts.Add(evt.EventId, summary);    
                    }
                    else
                    {
                        var summary = string.Format("{0}: {1}" , evt.DateTime.ToShortDateString(), evt.Name);
                        Evts.Add(evt.EventId, summary);    
                    }
                    
                }
            
                Comments = new MessagesViewModel(user.Community.Messages.OrderByDescending(m => m.MessageId).Take(5).ToList());
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
        public Dictionary<int, string> Neightbours { get; set; }
        public MessagesViewModel Comments { get; set; }
}
    }

    
