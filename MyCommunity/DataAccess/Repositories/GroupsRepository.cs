using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class GroupsRepository : Repository<Group>
    {
        public GroupsRepository(DB entities)
            : base(entities)
        {
        }
    }
}