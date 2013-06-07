using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class EventViewModel
    {
        public EventViewModel(Event evt)
        {
            Id = evt.EventId;
            Name = evt.Name??"None";
            Messages = new MessagesViewModel(evt.Messages.ToList());
            Date = evt.DateTime.ToShortDateString();
            Location = evt.Location?? "Unknown";
            Description = evt.Description ?? "None";
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public MessagesViewModel Messages { get; set; }
    }
}

    
    
        