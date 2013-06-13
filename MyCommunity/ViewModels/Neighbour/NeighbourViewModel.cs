using MyCommunity.Models;

namespace MyCommunity.ViewModels.Neighbour
{
    public class NeighbourViewModel : BaseViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Groups { get; set; }
        public int Campaigns { get; set; }

        public NeighbourViewModel With(UserProfile user)
        {
            ID = user.UserId;
            Name = user.DisplayName;
            Address = user.Address ?? "unknown";
            Groups = user.Groups.Count;
            Campaigns = user.Campaigns.Count;
            return this;
        }
    }
}