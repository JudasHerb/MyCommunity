using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class CampaignsRepository: Repository<Campaign>
    {
        public CampaignsRepository(UnitOfWork entities)
            : base(entities)
        {
        }
    }
}