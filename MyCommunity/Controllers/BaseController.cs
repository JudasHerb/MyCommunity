using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.Services;
using MyCommunity.ViewModels;
using WebMatrix.WebData;

namespace MyCommunity.Controllers
{
    public class BaseController : Controller
    {
        private readonly ISecurityService _securityService;


        public BaseController(ISecurityService securityService)
        {
            _securityService = securityService;
            
        }

        public T CreateViewModel<T>() where T : BaseViewModel, new()
        {
            if (_securityService.IsAuthenticated)
            {
                UserProfile user = _securityService.CurrentUser();

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