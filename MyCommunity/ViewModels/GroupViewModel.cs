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
            Id = group.GroupID;
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public MessagesViewModel Messages { get; set; }
    }
}