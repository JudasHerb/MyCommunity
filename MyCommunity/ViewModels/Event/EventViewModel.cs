namespace MyCommunity.ViewModels.Event
{
    public class EventViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public MessagesViewModel Messages { get; set; }

        public EventViewModel With(Models.Event evt)
        {
            Id = evt.EventId;
            Name = evt.Name ?? "None";
            Messages = new MessagesViewModel().With(evt.Messages).Using("Event", evt.EventId).ShowPostForm();
            Date = evt.DateTime.ToShortDateString();
            Location = evt.Location ?? "Unknown";
            Description = evt.Description ?? "None";
            return this;
        }
    }
}