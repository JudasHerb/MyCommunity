using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories


{
    public class CommunityRepository : Repository<Community>
    {
        public CommunityRepository(DB entities)
            : base(entities)
        {
        }
    }
}