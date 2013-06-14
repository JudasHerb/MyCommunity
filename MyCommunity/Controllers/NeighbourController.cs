using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.Services;
using MyCommunity.ViewModels.Messaging;
using MyCommunity.ViewModels.Neighbour;

namespace MyCommunity.Controllers
{
    public class NeighbourController : BaseController
    {
        private readonly ISecurityService _securityService;
        private readonly IMessageService _messageService;


        public NeighbourController(ISecurityService securityService, IMessageService messageService)
            : base(securityService)
        {
            _securityService = securityService;
            _messageService = messageService;
        }


        public ActionResult Index()
        {
            var community = _securityService.CurrentUserCommunity();

            return View(CreateViewModel<IndexViewModel>().With(community.Members));
        }

        public ActionResult Neighbour(int id)
        {
            var neighbour = _securityService.GetUser(id);

            return View(CreateViewModel<NeighbourViewModel>().With(neighbour));
        }

        [HttpPost]
        public ActionResult Message(MessageNeighbourViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = _messageService.CreateUserMessage(model.Comment, model.AddresseeId);
                
                return PartialView("_UserMessagePartial", new UserMessageViewModel(message,true));
            }

            return Json(null);
        }
    }
}