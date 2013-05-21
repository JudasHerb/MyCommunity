﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.DataAccess.Repositories
{
    public class UsersRepository : Repository<UserProfile>
    {
        public UsersRepository(UnitOfWork entities)
            : base(entities)
        {
        }

    }
}