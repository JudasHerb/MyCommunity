using System.Linq;
using MyCommunity.Models;
using WebMatrix.WebData;

namespace MyCommunity.DataAccess.Repositories
{
    public class UsersRepository : Repository<UserProfile>
    {
        public UsersRepository(DB entities)
            : base(entities)
        {
        }

        public UserProfile CurrentUser()
        {
            if (WebSecurity.IsAuthenticated)
            {
                return Find(u => u.Email == WebSecurity.CurrentUserName);
            }
            else
            {
                return default(UserProfile);
            }
        }
    }
}