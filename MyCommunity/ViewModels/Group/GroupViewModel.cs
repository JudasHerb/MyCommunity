using System.Collections.Generic;
using MyCommunity.Models;
using MyCommunity.ViewModels.Event;

namespace MyCommunity.ViewModels.Group
{
    public class GroupViewModel : BaseViewModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public MessagesViewModel Messages { get; set; }
        public Dictionary<int, string> Evts { get; set; }
        public Dictionary<int, string> Members { get; set; }
        public CreateEventViewModel CreateEventModel { get; set; }
        public bool IsPublic { get; set; }
        public bool IsSubscribed { get; set; }

        public GroupViewModel With(Models.Group group)
        {
            IsPublic = group.IsPublic;
            Name = group.Name;
            Messages = new MessagesViewModel().With(group.Messages).Using("Group", group.GroupId).ShowPostForm();
            Evts = new Dictionary<int, string>();
            Members = new Dictionary<int, string>();
            Id = group.GroupId;
            Description = group.Description;
            foreach (Models.Event evt in group.Events)
            {
                if (evt.Name.Length > 20)
                {
                    string summary = string.Format("{0}: {1} ...", evt.DateTime.ToShortDateString(),
                                                   evt.Name.Substring(0, 20));
                    Evts.Add(evt.EventId, summary);
                }
                else
                {
                    string summary = string.Format("{0}: {1}", evt.DateTime.ToShortDateString(), evt.Name);
                    Evts.Add(evt.EventId, summary);
                }
            }
            foreach (UserProfile mem in group.Members)
            {
                if (mem.DisplayName.Length > 20)
                {
                    string summary = string.Format("{0} ...", mem.DisplayName.Substring(0, 20));
                    Members.Add(mem.UserId, summary);
                }
                else
                {
                    Members.Add(mem.UserId, mem.DisplayName);
                }
            }
            CreateEventModel = new CreateEventViewModel { Ctrler = "Group", ObjId = group.GroupId };
            IsSubscribed = true;
            return this;
        }

        public GroupViewModel NotSubscribed()
        {
            IsSubscribed = false;
            Messages.ShowForm = false;
            return this;
        }
    }
}