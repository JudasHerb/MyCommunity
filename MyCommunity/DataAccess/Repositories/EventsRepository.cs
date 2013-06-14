using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class EventsRepository : Repository<Event>
    {
        public EventsRepository(DB entities)
            : base(entities)
        {
        }
    }
}