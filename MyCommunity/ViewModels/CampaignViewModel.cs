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
            Id = campaigns.CampaignID;
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public MessagesViewModel Messages { get; set; }
    }
}