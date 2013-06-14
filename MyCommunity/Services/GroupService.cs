using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.DataAccess;
using MyCommunity.Models;

namespace MyCommunity.Services
{
    public interface IGroupService
    {
        Group GetGroup(int id);
        Group CreateGroup(string name, string description, bool isPublic);
        void JoinGroup(int id);
        bool UserNeedsInviteTo(Group group);
        bool UserIsSubscribedTo(Group group);
    }

    public class GroupService:IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISecurityService _securityService;

        public GroupService(IUnitOfWork unitOfWork, ISecurityService securityService)
        {
            _unitOfWork = unitOfWork;
            _securityService = securityService;
        }

        public Group GetGroup(int id)
        {
            return _unitOfWork.GroupsRepository.Find(g => g.GroupId == id);
            
        }

        public Group CreateGroup(string name, string description, bool isPublic)
        {
            var newgroup = new Group
            {
                Name = name,
                Description = description,
                IsPublic = isPublic
            };

            var user = _securityService.CurrentUser();

            newgroup.Members.Add(user);

            user.Community.Groups.Add(newgroup);

            _unitOfWork.Save();

            return newgroup;
        }

        public void JoinGroup(int id)
        {
            var group = _unitOfWork.GroupsRepository.Find(c => c.GroupId == id);

            group.Members.Add(_securityService.CurrentUser());

            _unitOfWork.Save();
        }

        public bool UserNeedsInviteTo(Group group)
        {
            return !group.Members.Contains(_securityService.CurrentUser()) && !group.IsPublic;
        }

        public bool UserIsSubscribedTo(Group group)
        {
            return group.Members.Contains(_securityService.CurrentUser()) ;
        }
    }
}