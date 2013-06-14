using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.Services;
using MyCommunity.ViewModels.Messaging;

namespace MyCommunity.Controllers
{
    public class MessagingController : BaseController
    {
        private readonly ISecurityService _securityService;
        private readonly IMessageService _messageService;


        public MessagingController(ISecurityService securityService, IMessageService messageService)
            : base(securityService)
        {
            _securityService = securityService;
            _messageService = messageService;
        }

        public ActionResult Messages(int id)
        {
            var user = _securityService.CurrentUser();

            var participant = _securityService.GetUser(id);

            _messageService.MarkUsersMessagesAsReadByParticipant(user, participant);
            
            return View(CreateViewModel<UserMessagesViewModel>().With(user, participant));
        }
    }
}