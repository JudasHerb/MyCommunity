using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class CampaignsRepository : Repository<Campaign>
    {
        public CampaignsRepository(UnitOfWork entities)
            : base(entities)
        {
        }
    }
}