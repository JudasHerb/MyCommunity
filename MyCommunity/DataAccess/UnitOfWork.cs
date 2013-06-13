using System.Data.Entity;
using MyCommunity.DataAccess.Repositories;
using MyCommunity.Models;
//using MyCommunity.Migrations;

namespace MyCommunity.DataAccess
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        private CampaignsRepository _campaignsRepository;
        private CommunityRepository _communtiesRepository;
        private EventsRepository _eventsRepository;
        private GroupsRepository _groupsRepository;
        private UsersRepository _usersRepository;
        private UserMessageRepository _userMessageRepository;
        public UnitOfWork()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>()
                        .HasRequired(g => g.Community)
                        .WithMany(c => c.Groups)
                        .HasForeignKey(c => c.CommunityID)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Campaign>()
                        .HasRequired(g => g.Community)
                        .WithMany(c => c.Campaigns)
                        .HasForeignKey(c => c.CommunityID)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<UserProfile>()
                        .HasRequired(g => g.Community)
                        .WithMany(c => c.Members)
                        .HasForeignKey(c => c.CommunityID)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                        .HasRequired(m => m.From)
                        .WithMany(u => u.Posts)
                        .HasForeignKey(m => m.FromId)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Message>()
                        .HasOptional(m => m.Campaign)
                        .WithMany(u => u.Messages)
                        .HasForeignKey(m => m.CampaignId)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Message>()
                        .HasOptional(m => m.Event)
                        .WithMany(u => u.Messages)
                        .HasForeignKey(m => m.EventId)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Message>()
                        .HasOptional(m => m.Community)
                        .WithMany(u => u.Messages)
                        .HasForeignKey(m => m.CommunityId)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Message>()
                        .HasOptional(m => m.Group)
                        .WithMany(u => u.Messages)
                        .HasForeignKey(m => m.GroupId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                        .HasOptional(e => e.Group)
                        .WithMany(g => g.Events)
                        .HasForeignKey(e => e.GroupId)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Event>()
                        .HasOptional(e => e.Community)
                        .WithMany(g => g.Events)
                        .HasForeignKey(e => e.CommunityId)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Event>()
                        .HasOptional(e => e.Campaign)
                        .WithMany(g => g.Events)
                        .HasForeignKey(e => e.CampaignId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserMessage>()
                        .HasRequired(m => m.Sender)
                        .WithMany(u => u.SentMessages)
                        .HasForeignKey(e => e.SenderId)
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<UserMessage>()
                        .HasRequired(m => m.Addressee)
                        .WithMany(u => u.ReceivedMessages)
                        .HasForeignKey(e => e.AddresseeId)
                        .WillCascadeOnDelete(false);
        }

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

        public UserMessageRepository UserMessageRepository
        {
            get { return _userMessageRepository ?? (_userMessageRepository = new UserMessageRepository(this)); }
        }

        #endregion
    }
}