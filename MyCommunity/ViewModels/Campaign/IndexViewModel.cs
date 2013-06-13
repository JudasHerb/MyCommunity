using System.Collections.Generic;

namespace MyCommunity.ViewModels.Campaign
{
    public class IndexViewModel : BaseViewModel
    {
        public List<CampaignViewModel> Campaigns { get; set; }

        public IndexViewModel With(IEnumerable<Models.Campaign> campaigns)
        {
            Campaigns = new List<CampaignViewModel>();
            foreach (Models.Campaign camp in campaigns)
            {
                Campaigns.Add(new CampaignViewModel().With(camp));
            }
            return this;
        }
    }
}