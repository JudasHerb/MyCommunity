using MyCommunity.DataAccess.Repositories;

namespace MyCommunity.DataAccess
{
    public interface IUnitOfWork
    {
        UsersRepository UsersRepository { get; }
        CommunityRepository CommunitiesRepository { get; }
        GroupsRepository GroupsRepository { get; }
        CampaignsRepository CampaignsRepository { get; }
        EventsRepository EventsRepository { get; }
        UserMessageRepository UserMessageRepository { get;  }
        void Save();
    }
}