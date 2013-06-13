using System.Collections.Generic;
using MyCommunity.Models;
using MyCommunity.ViewModels.Event;

namespace MyCommunity.ViewModels.Campaign
{
    public class CampaignViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int CampaignId { get; set; }
        public MessagesViewModel Messages { get; set; }
        public Dictionary<int, string> Evts { get; set; }
        public string Description { get; set; }
        public Dictionary<int, string> Members { get; set; }
        public CreateEventViewModel CreateEventModel { get; set; }
        public bool IsSubscribed { get; set; }

        public CampaignViewModel With(Models.Campaign campaigns)
        {
            Name = campaigns.Name;

            Messages =
                new MessagesViewModel().With(campaigns.Messages).Using("Campaign", campaigns.CampaignId).ShowPostForm();

            Evts = new Dictionary<int, string>();

            Members = new Dictionary<int, string>();

            CampaignId = campaigns.CampaignId;

            foreach (Models.Event evt in campaigns.Events)
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

            foreach (UserProfile mem in campaigns.Members)
            {
                if (mem.DisplayName.Length > 20)
                {
                    string summary = string.Format("{0} ...", mem.DisplayName.Substring(0, 20));
                    Members.Add(mem.UserId, summary);
                }
                else
                {
                    Members.Add(mem.UserId, mem.DisplayName);
                }
            }

            Description = campaigns.Description;

            CreateEventModel = new CreateEventViewModel { Ctrler = "Campaign", ObjId = campaigns.CampaignId };

            IsSubscribed = true;

            return this;
        }

        public CampaignViewModel NotSubscribed()
        {
            IsSubscribed = false;
            Messages.ShowForm = false;
            return this;
        }
    }
}