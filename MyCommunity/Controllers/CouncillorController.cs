using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.Services;
using MyCommunity.ViewModels.Councillor;

namespace MyCommunity.Controllers
{
    public class CouncillorController : BaseController
    {
        private readonly ISecurityService _securityService;
        

        public CouncillorController(ISecurityService securityService)
            : base(securityService)
        {
            _securityService = securityService;
            
        }

        public ActionResult Index()
        {
            var community = _securityService.CurrentUserCommunity();

            return View(CreateViewModel<IndexViewModel>().With(community.Councillors));
        }
    }
}