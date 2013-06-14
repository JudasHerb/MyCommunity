using System;
using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.Services;
using MyCommunity.ViewModels;
using MyCommunity.ViewModels.Event;
using MyCommunity.ViewModels.Group;

namespace MyCommunity.Controllers
{
    public class GroupController : BaseController
    {
        private readonly ISecurityService _securityService;
        private readonly IGroupService _groupService;
        
        private readonly IEventService _eventService;
        private readonly IMessageService _messageService;


        public GroupController(ISecurityService securityService, IGroupService groupService, IEventService eventService, IMessageService messageService)
            : base(securityService)
        {
            _securityService = securityService;
            _groupService = groupService;
            
            _eventService = eventService;
            _messageService = messageService;
        }

        public ActionResult Group(int id)
        {
            var group = _groupService.GetGroup(id);

            if (_groupService.UserNeedsInviteTo(group)) return View("PrivateGroup",CreateViewModel<PrivateGroupViewModel>());

            var model = CreateViewModel<GroupViewModel>().With(group);

            if (!_groupService.UserIsSubscribedTo(group)) model = model.NotSubscribed();

            return View(model);
        }

        public ActionResult CreateGroupPartial()
        {
            return PartialView("_CreateGroupPartial");
        }

        [HttpPost]
        public ActionResult Group(CreateGroupViewModel group)
        {
            if (ModelState.IsValid)
            {
                var newGroup = _groupService.CreateGroup(group.Name, group.Description, group.IsPublic);

                return Json(new { state = "Success", additional = Url.Action("Group", new { id = newGroup.GroupId }) });
            }

            return Json(new {state = "Fail", additional = "Need to fill in all data"});
        }

        public ActionResult Index()
        {
            var user = _securityService.CurrentUser();

            return View(CreateViewModel<IndexViewModel>().With(user.Community.Groups,user));
        }

        public ActionResult JoinGroup(int Id)
        {
            _groupService.JoinGroup(Id);

            return RedirectToAction("Group", new {id = Id});
        }

        [HttpPost]
        public ActionResult Event(CreateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newEvent = _eventService.CreateGroupEvent(model.ObjId,model.Name, DateTime.Parse(model.Dt), model.Location, model.Description);

                return Json(new {state = "Success", additional = Url.Action("Event", "Event", new {id = newEvent.EventId})});
            }

            return Json(new {state = "Fail", additional = "Need to fill in all data"});
        }

        public ActionResult MoreComments(int count, int Id)
        {
            var nextMessage = _messageService.PageGroupMessage(count, Id);

            if (nextMessage != null)
            {
                return PartialView("_MessagePartial", CreateViewModel<MessageViewModel>().With(nextMessage));
            }

            return Json(null);
        }

        [HttpPost]
        public ActionResult Comment(string comment, int Id)
        {
            var message = _messageService.CreateGroupMessage(comment, Id);
            
            return PartialView("_MessagePartial", CreateViewModel<MessageViewModel>().With(message));
        }
    }
}