using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.Services;
using MyCommunity.ViewModels.MP;

namespace MyCommunity.Controllers
{
    public class MPController : BaseController
    {
        private readonly ISecurityService _securityService;
        

        public MPController(ISecurityService securityService)
            : base(securityService)
        {
            _securityService = securityService;
        
        }

        public ActionResult MP()
        {
            var community = _securityService.CurrentUserCommunity();
            return View(CreateViewModel<MPViewModel>().With(community.MP));
        }
    }
}