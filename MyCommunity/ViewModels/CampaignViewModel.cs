using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class CampaignViewModel
    {
        public CampaignViewModel(Campaign campaigns)
        {
            Name = campaigns.Name;
            Messages = new MessagesViewModel(campaigns.Messages.Take(5).ToList());
            Evts = new Dictionary<int, string>();
            Members = new Dictionary<int, string>();
            Id = campaigns.CampaignId;
            foreach (var evt in campaigns.Events)
            {
                if (evt.Name.Length > 10)
                {
                    var summary = string.Format("{0}: {1} ...", evt.DateTime.ToShortDateString(), evt.Name.Substring(0, 10));
                    Evts.Add(evt.EventId, summary);
                }
                else
                {
                    var summary = string.Format("{0}: {1}", evt.DateTime.ToShortDateString(), evt.Name);
                    Evts.Add(evt.EventId, summary);
                }

            }
            foreach (var mem in campaigns.Members)
            {
                if (mem.DisplayName.Length > 10)
                {
                    var summary = string.Format("{0} ...", mem.DisplayName.Substring(0, 10));
                    Members.Add(mem.UserId, summary);
                }
                else
                {

                    Members.Add(mem.UserId, mem.DisplayName);
                }
            }
            Description = campaigns.Description;
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public MessagesViewModel Messages { get; set; }
        public Dictionary<int, string> Evts { get; set; }
        public string Description { get; set; }
        public Dictionary<int, string> Members { get; set; }
    }
}