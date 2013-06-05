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
            Groups = new List<GroupViewModel>();
            foreach (var grp in campaigns)
            {
                Groups.Add(new GroupViewModel(grp));
            }
        }
        public List<GroupViewModel> Groups { get; set; }
    }
}