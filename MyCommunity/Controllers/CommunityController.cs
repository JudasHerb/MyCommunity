using System;
using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.Services;
using MyCommunity.ViewModels;
using MyCommunity.ViewModels.Community;
using MyCommunity.ViewModels.Event;

namespace MyCommunity.Controllers
{
    public class CommunityController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly ISecurityService _securityService;
        private readonly IEventService _eventService;


        public CommunityController(IMessageService messageService,ISecurityService securityService, IEventService eventService)
            : base(securityService)
        {
            _messageService = messageService;
            _securityService = securityService;
            _eventService = eventService;
        }

        public ActionResult Index()
        {
            var user = _securityService.CurrentUser();

            return View(CreateViewModel<IndexViewModel>().With(user));
        }

        public ActionResult MoreComments(int count, int Id)
        {
            var nextMessage = _messageService.PageCommunityMessage(count, Id);

            if (nextMessage != null)
            {
                return PartialView("_MessagePartial", CreateViewModel<MessageViewModel>().With(nextMessage));
            }

            return Json(null);
        }

        [HttpPost]
        public ActionResult Comment(string comment, int Id)
        {
            var message = _messageService.CreateCommunityMessage(comment, Id);

            return PartialView("_MessagePartial", CreateViewModel<MessageViewModel>().With(message));
        }

        [HttpPost]
        public ActionResult Event(CreateEventViewModel group)
        {
            if (ModelState.IsValid)
            {
                var newEvent = _eventService.CreateCommunityEvent(group.ObjId,group.Name, DateTime.Parse(group.Dt), group.Location, group.Description);

                return Json(new {state = "Success", additional = Url.Action("Event", "Event", new {id = newEvent.EventId})});
            }
            else
            {
                return Json(new {state = "Fail", additional = "Need to fill in all data"});
            }
        }

        public ActionResult Events()
        {
            var community = _securityService.CurrentUserCommunity();

            return View(CreateViewModel<BrowseEventsViewModel>().With(community.Events, community.CommunityId));
        }
    }
}