using System.Linq;
using MyCommunity.Models;
using WebMatrix.WebData;

namespace MyCommunity.DataAccess.Repositories
{
    public class UsersRepository : Repository<UserProfile>
    {
        public UsersRepository(UnitOfWork entities)
            : base(entities)
        {
        }

        public UserProfile CurrentUser()
        {
            if (WebSecurity.IsAuthenticated)
            {
                return FindBy(u => u.Email == WebSecurity.CurrentUserName).First();
            }
            else
            {
                return default(UserProfile);
            }
        }
    }
}