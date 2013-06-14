using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.DataAccess;
using MyCommunity.Models;

namespace MyCommunity.Services
{
    public interface IEventService
    {
        Event CreateCommunityEvent(int communityId, string name, DateTime date, string location, string description);
        Event CreateCampaignEvent(int campaignId, string name, DateTime date, string location, string description);
        Event CreateGroupEvent(int groupid, string name, DateTime dt, string location, string description);
        Event GetEvent(int id);
    }

    public class EventService:IEventService
    {
        private readonly ISecurityService _securityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICampaignService _campaignService;
        private readonly IGroupService _groupService;

        public EventService(ISecurityService securityService, IUnitOfWork unitOfWork, ICampaignService campaignService, IGroupService groupService)
        {
            _securityService = securityService;
            _unitOfWork = unitOfWork;
            _campaignService = campaignService;
            _groupService = groupService;
        }

        public Event CreateCommunityEvent(int communityId,string name, DateTime date, string location, string description)
        {
            var newEvent = CreateEvent(name, date, location, description);

            var community = _securityService.CurrentUserCommunity();

            community.Events.Add(newEvent);

            _unitOfWork.Save();

            return newEvent;
        }
        private Event CreateEvent(string name, DateTime date, string location, string description)
        {
            return new Event
            {
                Name = name,
                DateTime = date,
                Location = location,
                Description = description
            };
        }

        public Event GetEvent(int id)
        {
            return _unitOfWork.EventsRepository.Find(e => e.EventId == id);
        }


        public Event CreateCampaignEvent(int campaignId, string name, DateTime date, string location, string description)
        {
            var campaign = _campaignService.GetCampaign(campaignId);

            var newEvent = CreateEvent(name, date, location, description);

            campaign.Events.Add(newEvent);

            _unitOfWork.Save();

            return newEvent;
        }

        public Event CreateGroupEvent(int groupId, string name, DateTime date, string location, string description)
        {

            var group = _groupService.GetGroup(groupId);

            var newEvent = CreateEvent(name, date, location, description);

            group.Events.Add(newEvent);

            _unitOfWork.Save();

            return newEvent;
        }
    }
}