using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class BrowseGroupsViewModel
    {
        public BrowseGroupsViewModel(IList<Groups> campaigns)
        {
            Groups = new Dictionary<int, string>();
            foreach (var camp in campaigns)
            {
                Groups.Add(camp.GroupID, camp.Name);
            }
        }
        public Dictionary<int, string> Groups { get; set; }
    }
}