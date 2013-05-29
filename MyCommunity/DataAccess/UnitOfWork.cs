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
        private GroupsRepository _groupsRepository;
        private CampaignsRepository _campaignsRepository;
        private EventsRepository _eventsRepository;
        
        public UnitOfWork()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<Campaigns> Campaigns { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Events> Events { get; set; }
    

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

        public GroupsRepository GroupsRepository
        {
            get { return _groupsRepository ?? (_groupsRepository = new GroupsRepository(this)); }
        }
        public CampaignsRepository CampaignsRepository
        {
            get { return _campaignsRepository ?? (_campaignsRepository = new CampaignsRepository(this)); }
        }

        public EventsRepository EventsRepository
        {
            get { return _eventsRepository ?? (_eventsRepository = new EventsRepository(this)); }
        }


        #endregion
    }
}
