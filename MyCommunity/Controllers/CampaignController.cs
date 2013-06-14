using System;
using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.Services;
using MyCommunity.ViewModels;
using MyCommunity.ViewModels.Campaign;
using MyCommunity.ViewModels.Event;

namespace MyCommunity.Controllers
{
    public class CampaignController : BaseController
    {
        private readonly ISecurityService _securityService;
        private readonly ICampaignService _campaignService;
        private readonly IEventService _eventService;
        private readonly IMessageService _messageService;


        public CampaignController(ISecurityService securityService, ICampaignService campaignService, IEventService eventService, IMessageService messageService)
            : base(securityService)
        {
            _securityService = securityService;
            _campaignService = campaignService;
            _eventService = eventService;
            _messageService = messageService;
        }

        public ActionResult Campaign(int id)
        {
            var campaign = _campaignService.GetCampaign(id);
            
            var model = CreateViewModel<CampaignViewModel>().With(campaign);

            if (!_campaignService.IsUserSubscribedTo(campaign)) model = model.NotSubscribed();

            return View(model);
        }

        public ActionResult CreateCampaignPartial()
        {
            return PartialView("_CreateCampaignPartial");
        }

        [HttpPost]
        public ActionResult Campaign(CreateCampaignViewModel campaign)
        {
            if (ModelState.IsValid)
            {
                var newCampaign = _campaignService.CreateCampaign(campaign.Name, campaign.Description);

                return Json(new {state = "Success", additional = Url.Action("Campaign", new {id = newCampaign.CampaignId})});
            }

            return Json(new {state = "Fail", additional = "Need to fill in all data"});
        }

        public ActionResult Index()
        {
            return View(CreateViewModel<IndexViewModel>().With(_securityService.CurrentUserCommunity().Campaigns));
        }

        public ActionResult JoinCampaign(int Id)
        {
            _campaignService.JoinCampaign(Id);
            
            return RedirectToAction("Campaign", new {id = Id});
        }

        [HttpPost]
        public ActionResult Event(CreateEventViewModel group)
        {
            if (ModelState.IsValid)
            {
                var newEvent = _eventService.CreateCampaignEvent(group.ObjId,group.Name, DateTime.Parse(group.Dt), group.Location, group.Description);

                return Json(new {state = "Success", additional = Url.Action("Event", "Event", new {id = newEvent.EventId})});
            }
            else
            {
                return Json(new {state = "Fail", additional = "Need to fill in all data"});
            }
        }

        public ActionResult MoreComments(int count, int Id)
        {
            return Json(null);
        }

        [HttpPost]
        public ActionResult Comment(string comment, int Id)
        {
            var message = _messageService.CreateCampaignMessage(comment, Id);

            return PartialView("_MessagePartial", CreateViewModel<MessageViewModel>().With(message));
        }
    }
}