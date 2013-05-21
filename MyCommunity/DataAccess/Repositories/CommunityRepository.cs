using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories 
     

{
    public class CommunityRepository: Repository<Community>
    {
        public CommunityRepository(UnitOfWork entities)
            : base(entities)
        {
        }

    }
}