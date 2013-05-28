using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.DataAccess.Repositories;

namespace MyCommunity.DataAccess
{
    public interface IUnitOfWork
    {
        UsersRepository UsersRepository { get; }
        CommunityRepository CommunitiesRepository { get; }
        
        void Save();
    }
}