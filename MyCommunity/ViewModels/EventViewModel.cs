using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class EventViewModel
    {
        public EventViewModel(Events evt)
        {
            Name = evt.Name;
        }
        public string Name { get; set; }
    }
}