using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using WebMatrix.WebData;

namespace MyCommunity.Services
{
    public interface ISecurityService
    {
        bool IsAuthenticated { get;  }
        UserProfile CurrentUser();
        Community CurrentUserCommunity();
        UserProfile GetUser(int id);
        Community DefaultCommunity();
        void AssignToDefaultCommunityByMail(string email);
    }

    public class SecurityService:ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsAuthenticated { get { return WebSecurity.IsAuthenticated; } }

        public UserProfile CurrentUser()
        {
            return IsAuthenticated ? _unitOfWork.UsersRepository.Find(u => u.Email == WebSecurity.CurrentUserName) : default(UserProfile);
        }

        public Community CurrentUserCommunity()
        {
            return CurrentUser().Community;
        }

        public UserProfile GetUser(int id)
        {
            return _unitOfWork.UsersRepository.Find(u => u.UserId == id);
        }


        public Community DefaultCommunity()
        {
            return _unitOfWork.CommunitiesRepository.All().First();
        }

        public void AssignToDefaultCommunityByMail(string email)
        {
            var user = _unitOfWork.UsersRepository.Find(u => u.Email == email);

            var community = DefaultCommunity();

            user.Community = community;

            community.Members.Add(user);

            _unitOfWork.Save();
        }
    }
}