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
            Neighbours = new Dictionary<int, string>();
            foreach (var user in members.Take(5))
            {
                Neighbours.Add(user.UserId, user.DisplayName);
            }
        }

        public Dictionary<int, string> Neighbours { get; set; }
    }
    
}