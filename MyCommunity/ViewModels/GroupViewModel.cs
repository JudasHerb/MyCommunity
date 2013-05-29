using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class GroupViewModel
    {
        public GroupViewModel(Groups group)
        {
            Name = group.Name;
            Messages = new MessagesViewModel(group.Messages.Take(5).ToList());
            Evts = new Dictionary<int, string>();
            Id = group.GroupID;
            foreach (var evt in group.Events)
            {
                Evts.Add(evt.EventID, evt.Name);
            }
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public MessagesViewModel Messages { get; set; }
        public Dictionary<int, string> Evts { get; set; }
    }
}