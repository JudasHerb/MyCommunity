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
            Members = new Dictionary<int, string>();
            Id = group.GroupID;
            Description = group.Description;
            foreach (var evt in group.Events)
            {
                if (evt.Name.Length > 10)
                {
                    var summary = string.Format("{0}: {1} ...", evt.DateTime.ToShortDateString(), evt.Name.Substring(0, 10));
                    Evts.Add(evt.EventID, summary);
                }
                else
                {
                    var summary = string.Format("{0}: {1}", evt.DateTime.ToShortDateString(), evt.Name);
                    Evts.Add(evt.EventID, summary);
                }
                
            }
            foreach (var mem in group.Members)
            {
                if (mem.UserName.Length > 10)
                {
                    var summary = string.Format("{0} ...", mem.UserName.Substring(0, 10));
                    Members.Add(mem.UserId, summary);
                }
                else
                {
                    
                    Members.Add(mem.UserId, mem.UserName );
                }
            }
        }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public MessagesViewModel Messages { get; set; }
        public Dictionary<int, string> Evts { get; set; }
        public Dictionary<int, string> Members { get; set; }
    }
}