using System;
using MyCommunity.DataAccess.Repositories;

//using MyCommunity.Migrations;

namespace MyCommunity.DataAccess
{
    public class UnitOfWork :  IUnitOfWork
    {
        private CampaignsRepository _campaignsRepository;
        private CommunityRepository _communtiesRepository;
        private EventsRepository _eventsRepository;
        private GroupsRepository _groupsRepository;
        private UsersRepository _usersRepository;
        private UserMessageRepository _userMessageRepository;
        private MessagesRepository _messagesRepository;

        private DB _dbContext;

        public UnitOfWork()
        {
            _dbContext = new DB();
        }
        
        #region IUnitOfWork Members

        public void Save()
        {
            _dbContext.SaveChanges();
        }


        public UsersRepository UsersRepository
        {
            get { return _usersRepository ?? (_usersRepository = new UsersRepository(_dbContext)); }
        }

        public CommunityRepository CommunitiesRepository
        {
            get { return _communtiesRepository ?? (_communtiesRepository = new CommunityRepository(_dbContext)); }
        }

        public GroupsRepository GroupsRepository
        {
            get { return _groupsRepository ?? (_groupsRepository = new GroupsRepository(_dbContext)); }
        }

        public CampaignsRepository CampaignsRepository
        {
            get { return _campaignsRepository ?? (_campaignsRepository = new CampaignsRepository(_dbContext)); }
        }

        public EventsRepository EventsRepository
        {
            get { return _eventsRepository ?? (_eventsRepository = new EventsRepository(_dbContext)); }
        }

        public UserMessageRepository UserMessageRepository
        {
            get { return _userMessageRepository ?? (_userMessageRepository = new UserMessageRepository(_dbContext)); }
        }
        public MessagesRepository MessagesRepository { get { return _messagesRepository ?? (_messagesRepository = new MessagesRepository(_dbContext)); } }

        public void Dispose()
        {
            if (_usersRepository != null)
                _usersRepository.Dispose();
            if (_communtiesRepository != null)
                _communtiesRepository.Dispose();
            if (_groupsRepository != null)
                _groupsRepository.Dispose();
            if (_campaignsRepository != null)
                _campaignsRepository.Dispose();
            if (_eventsRepository != null)
                _eventsRepository.Dispose();
            if (_userMessageRepository != null)
                _userMessageRepository.Dispose();
            if (_messagesRepository != null)
                _messagesRepository.Dispose();
            if (_dbContext != null)
                _dbContext.Dispose();

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}