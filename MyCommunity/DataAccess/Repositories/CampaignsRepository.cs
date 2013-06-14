using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class CampaignsRepository : Repository<Campaign>
    {
        public CampaignsRepository(DB entities)
            : base(entities)
        {
        }
    }
}