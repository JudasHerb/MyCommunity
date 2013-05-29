using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class BrowseEventsViewModel
    {
        public BrowseEventsViewModel(IList<Events> events)
        {
            Evts = new Dictionary<int, string>();
            foreach (var evt in events)
            {
                Evts.Add(evt.EventID, evt.Name);
            }
        }

        public Dictionary<int, string> Evts { get; set; }

    }
}