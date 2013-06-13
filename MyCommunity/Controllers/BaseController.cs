using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.ViewModels;
using WebMatrix.WebData;

namespace MyCommunity.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T CreateViewModel<T>() where T : BaseViewModel, new()
        {
            if (WebSecurity.IsAuthenticated)
            {
                UserProfile user = _unitOfWork.UsersRepository.CurrentUser();

                var viewModel = new T
                    {
                        UserName = user.DisplayName,
                        CommunityName = string.Format(" - {0}", user.Community.Name),
                        IsAuthenticated = true,
                    };
                return viewModel;
            }
            else
            {
                var viewModel = new T
                    {
                        IsAuthenticated = false,
                    };
                return viewModel;
            }
        }
    }
}