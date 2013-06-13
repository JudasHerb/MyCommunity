using System.Collections.Generic;

namespace MyCommunity.ViewModels.Event
{
    public class BrowseEventsViewModel : BaseViewModel
    {
        public CreateEventViewModel CreateEventModel { get; set; }
        public List<EventViewModel> Events { get; set; }

        public BrowseEventsViewModel With(IEnumerable<Models.Event> events, int CommunityId)
        {
            Events = new List<EventViewModel>();
            foreach (Models.Event evt in events)
            {
                Events.Add(new EventViewModel().With(evt));
            }

            CreateEventModel = new CreateEventViewModel { Ctrler = "Community", ObjId = CommunityId };
            return this;
        }
    }
}