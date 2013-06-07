using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class NeighboursViewModel
    {
        public NeighboursViewModel(IList<UserProfile> members)
        {
            Neighbours = new List<NeighbourViewModel>();
            foreach (var user in members)
            {
                Neighbours.Add(new NeighbourViewModel(user) );
            }
        }

        public List<NeighbourViewModel> Neighbours { get; set; }
    }
    
}