using System.Collections.Generic;
using MyCommunity.Models;

namespace MyCommunity.ViewModels.Neighbour
{
    public class IndexViewModel : BaseViewModel
    {
        public List<NeighbourViewModel> Neighbours { get; set; }

        public IndexViewModel With(IEnumerable<UserProfile> members)
        {
            Neighbours = new List<NeighbourViewModel>();
            foreach (UserProfile user in members)
            {
                Neighbours.Add(new NeighbourViewModel().With(user));
            }
            return this;
        }
    }
}