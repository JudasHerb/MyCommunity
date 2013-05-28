using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MyCommunity.Models;

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
            var user = Membership.GetUser();
            return FindBy(u => u.UserName == user.UserName).First();
        }
    }
}