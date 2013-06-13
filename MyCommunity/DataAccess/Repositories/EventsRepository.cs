using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class EventsRepository : Repository<Event>
    {
        public EventsRepository(UnitOfWork entities)
            : base(entities)
        {
        }
    }
}