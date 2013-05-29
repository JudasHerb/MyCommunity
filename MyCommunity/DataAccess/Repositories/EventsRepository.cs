using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class EventsRepository: Repository<Events>
    {
        public EventsRepository(UnitOfWork entities)
            : base(entities)
        {
        }
    }
}