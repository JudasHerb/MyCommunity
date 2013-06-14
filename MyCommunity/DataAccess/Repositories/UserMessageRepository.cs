using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class UserMessageRepository: Repository<UserMessage>
    {
        public UserMessageRepository(DB entities)
            : base(entities)
        {
        }
    }
}