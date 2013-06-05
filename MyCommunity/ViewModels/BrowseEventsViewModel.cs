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
            Events = new List<EventViewModel>();
            foreach (var evt in events)
            {
                Events.Add(new EventViewModel(evt));
            }
        }

        public List<EventViewModel> Events { get; set; }

    }
}