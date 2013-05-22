using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class PartialGroupsViewModel
    {
        public PartialGroupsViewModel(IList<Groups> groups)
        {
            Groups = new Dictionary<int, string>();
            foreach (var group in groups.Take(5))
            {
                Groups.Add(group.GroupID, group.Name);
            }
        }
        public Dictionary<int, string> Groups { get; set; }
    }
}