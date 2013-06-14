using System.Linq;
using MyCommunity.DataAccess;
using MyCommunity.Models;

namespace MyCommunity.Services
{
    public interface IMessageService
    {
        Message CreateCommunityMessage(string comment, int id);
        Message PageCommunityMessage(int count, int id);
        Message CreateEventMessage(string comment, int id);
        Message CreateCampaignMessage(string comment, int id);
        Message PageGroupMessage(int count, int id);
        Message CreateGroupMessage(string comment, int id);
        void MarkUsersMessagesAsReadByParticipant(UserProfile user, UserProfile participant);
        UserMessage CreateUserMessage(string comment, int addresseeId);
    }
    public class MessageService:IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISecurityService _securityService;
        private readonly IEventService _eventService;
        private readonly IGroupService _groupService;
        private readonly ICampaignService _campaignService;

        public MessageService(IUnitOfWork unitOfWork, ISecurityService securityService, IEventService eventService, IGroupService groupService, ICampaignService campaignService)
        {
            _unitOfWork = unitOfWork;
            _securityService = securityService;
            _eventService = eventService;
            _groupService = groupService;
            _campaignService = campaignService;
        }

        public Message CreateCommunityMessage(string comment, int id)
        {
            var user = _securityService.CurrentUser();

            var message = new Message { Content = comment, From = user };

            user.Community.Messages.Add(message);

            _unitOfWork.Save();

            return message;
        }


        public Message PageCommunityMessage(int count, int id)
        {
            var community = _securityService.CurrentUserCommunity();

            if ((4 + count) < community.Messages.Count)
            {
                return community.Messages.Reverse().Skip(4 + count).FirstOrDefault();

            }
            return null;
        }


        public Message CreateEventMessage(string comment, int id)
        {
            var evt = _eventService.GetEvent(id);

            var user = _securityService.CurrentUser();

            var message = new Message { Content = comment, From = user};

            evt.Messages.Add(message);

            _unitOfWork.Save();

            return message;
        }


        public Message CreateCampaignMessage(string comment, int id)
        {
            var campaign = _campaignService.GetCampaign(id);

            var user = _securityService.CurrentUser();

            var message = new Message { Content = comment, From = user };

            campaign.Messages.Add(message);

            _unitOfWork.Save();

            return message;
        }

        public Message PageGroupMessage(int count, int id)
        {
            var group = _groupService.GetGroup(id);

            if ((4 + count) < group.Messages.Count)
            {
                return group.Messages.Reverse().Skip(4 + count).FirstOrDefault();
                
            }
            return null;

        }

        public Message CreateGroupMessage(string comment, int id)
        {
            var group = _groupService.GetGroup(id);

            var user = _securityService.CurrentUser();

            var message = new Message { Content = comment, From = user };

            group.Messages.Add(message);

            _unitOfWork.Save();

            return message;
        }

        public void MarkUsersMessagesAsReadByParticipant(UserProfile user, UserProfile participant)
        {
            if (participant != null)
            {

                foreach (UserMessage msg in user.ReceivedMessages.Where(m => !m.IsRead && m.SenderId == participant.UserId))
                {
                    msg.IsRead = true;
                }


                _unitOfWork.Save();
            }

        }

        public UserMessage CreateUserMessage(string comment, int addresseeId)
        {

            var neighbour = _securityService.GetUser(addresseeId);

            var message = new UserMessage
            {
                Content = comment,
                Sender = _securityService.CurrentUser(),
                Addressee = neighbour
            };

            _unitOfWork.UserMessageRepository.Create(message);

            _unitOfWork.Save();

            return message;
        }
    }
}