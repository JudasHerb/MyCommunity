using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class GroupRepository: Repository<Groups>
    {
        public GroupRepository(UnitOfWork entities)
            : base(entities)
        {
        }

    }
}