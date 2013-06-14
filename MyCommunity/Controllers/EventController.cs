using System;
using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.Services;
using MyCommunity.ViewModels;
using MyCommunity.ViewModels.Event;

namespace MyCommunity.Controllers
{
    public class EventController : BaseController
    {
        private readonly ISecurityService _securityService;
        private readonly IEventService _eventService;
        private readonly IMessageService _messageService;


        public EventController(ISecurityService securityService, IEventService eventService, IMessageService messageService)
            : base(securityService)
        {
            _securityService = securityService;
            _eventService = eventService;
            _messageService = messageService;
        }

        public ActionResult Event(int id)
        {
            var evt = _eventService.GetEvent(id);

            if (evt == null) RedirectToAction("Index", "Community");

            return View(CreateViewModel<EventViewModel>().With(evt));
        }

        public ActionResult CreateEventPartial(CreateEventViewModel model)
        {
            model.Dt = DateTime.Now.ToShortDateString();

            return PartialView("_CreateEventPartial", model);
        }

        public ActionResult MoreComments(int count, int Id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Comment(string comment, int Id)
        {
            var message = _messageService.CreateEventMessage(comment, Id);

            return PartialView("_MessagePartial", CreateViewModel<MessageViewModel>().With(message));
        }
    }
}