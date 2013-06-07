using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class NeighbourViewModel
    {
        public NeighbourViewModel(UserProfile user)
        {
            ID = user.UserId;
            Name = user.DisplayName;
            Address = user.Address ?? "unknown";
            Groups = user.Groups.Count;
            Campaigns = user.Campaigns.Count;
            
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Groups { get; set; }
        public int Campaigns { get; set; }

    }
}