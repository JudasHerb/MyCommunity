using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.DataAccess;
using MyCommunity.Models;

namespace MyCommunity.Services
{
    public interface ICampaignService
    {
        Campaign GetCampaign(int id);
        Campaign CreateCampaign(string name, string description);
        void JoinCampaign(int id);
        bool IsUserSubscribedTo(Campaign campaign);
    }

    public class CampaignService:ICampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISecurityService _securityService;

        public CampaignService(IUnitOfWork unitOfWork, ISecurityService securityService)
        {
            _unitOfWork = unitOfWork;
            _securityService = securityService;
        }

        public Campaign GetCampaign(int id)
        {
            return _unitOfWork.CampaignsRepository.Find(c => c.CampaignId == id);
        }


        public Campaign CreateCampaign(string name, string description)
        {
            var newCampaign = new Campaign {Name = name, Description = description,};

            var currentUser = _securityService.CurrentUser();

            newCampaign.Members.Add(currentUser);

            currentUser.Community.Campaigns.Add(newCampaign);

            _unitOfWork.Save();

            return newCampaign;
        }


        public void JoinCampaign(int id)
        {
            var campaign = _unitOfWork.CampaignsRepository.Find(c => c.CampaignId == id);

            campaign.Members.Add(_securityService.CurrentUser());

            _unitOfWork.Save();
        }


        public bool IsUserSubscribedTo(Campaign campaign)
        {
            return campaign.Members.Contains(_securityService.CurrentUser());
        }
    }
}