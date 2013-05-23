using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Security;
using MyCommunity.DataAccess.Repositories;
//using MyCommunity.Migrations;
using MyCommunity.Migrations;
using MyCommunity.Models;

namespace MyCommunity.DataAccess
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        private UsersRepository _usersRepository;
        private CommunityRepository _communtiesRepository;
        private GroupRepository _groupRepository;
        public UnitOfWork()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<Groups> Groups { get; set; }

        #region IUnitOfWork Members
  
        public void Save()
        {
            SaveChanges();
        }
        

        public UsersRepository UsersRepository
        {
            get { return _usersRepository ?? (_usersRepository = new UsersRepository(this)); }
        }
        public CommunityRepository CommunitiesRepository
        {
            get { return _communtiesRepository ?? (_communtiesRepository = new CommunityRepository(this)); }
        }
        public GroupRepository GroupRepository
        {
            get { return _groupRepository ?? (_groupRepository = new GroupRepository(this)); }
        }
        
        #endregion
    }
}
