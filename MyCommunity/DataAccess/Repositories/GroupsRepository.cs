using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class GroupsRepository: Repository<Group>
    {
        public GroupsRepository(UnitOfWork entities)
            : base(entities)
        {
        }
    }
}