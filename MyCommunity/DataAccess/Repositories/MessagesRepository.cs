using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class MessagesRepository: Repository<Message>
    {
        public MessagesRepository(DB entities)
            : base(entities)
        {
        }
    }
}