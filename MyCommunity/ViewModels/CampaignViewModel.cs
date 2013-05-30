using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class CampaignViewModel
    {
        public CampaignViewModel(Campaigns campaigns)
        {
            Name = campaigns.Name;
            Messages = new MessagesViewModel(campaigns.Messages.Take(5).ToList());
            Evts = new Dictionary<int, string>();
            Id = campaigns.CampaignID;
            foreach (var evt in campaigns.Events)
            {
                Evts.Add(evt.EventID, evt.Name);
            }
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public MessagesViewModel Messages { get; set; }
        public Dictionary<int, string> Evts { get; set; }
    }
}