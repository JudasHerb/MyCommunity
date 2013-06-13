using System;
using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.ViewModels;
using MyCommunity.ViewModels.Campaign;
using MyCommunity.ViewModels.Event;

namespace MyCommunity.Controllers
{
    public class CampaignController : BaseController
    {
        private readonly UserProfile _user;

        public CampaignController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _user = _unitOfWork.UsersRepository.CurrentUser();
        }

        public ActionResult Campaign(int id)
        {
            Campaign campaign = _unitOfWork.CampaignsRepository.FindBy(c => c.CampaignId == id).FirstOrDefault();

            if (campaign == null) RedirectToAction("Index", "Community");

            CampaignViewModel model = CreateViewModel<CampaignViewModel>().With(campaign);

            if (!campaign.Members.Contains(_user))
            {
                model = model.NotSubscribed();
            }

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
                var newCampaign = new Campaign
                    {
                        Name = campaign.Name,
                        Description = campaign.Description,
                    };

                newCampaign.Members.Add(_user);

                _user.Community.Campaigns.Add(newCampaign);

                _unitOfWork.Save();

                return Json(
                    new
                        {
                            state = "Success",
                            additional = Url.Action("Campaign", new {id = newCampaign.CampaignId})
                        });
            }

            return Json(new {state = "Fail", additional = "Need to fill in all data"});
        }

        public ActionResult Index()
        {
            return View(CreateViewModel<IndexViewModel>().With(_user.Community.Campaigns));
        }



        public ActionResult JoinCampaign(int Id)
        {
            Campaign campaign = _unitOfWork.CampaignsRepository.FindBy(c => c.CampaignId == Id).FirstOrDefault();

            if (campaign == null) RedirectToAction("Index", "Community");

            campaign.Members.Add(_user);

            _unitOfWork.Save();

            return RedirectToAction("Campaign", new {id = Id});
        }

        [HttpPost]
        public ActionResult Event(CreateEventViewModel group)
        {
            if (ModelState.IsValid)
            {
                Campaign campaign =
                    _unitOfWork.CampaignsRepository.FindBy(g => g.CampaignId == group.ObjId).FirstOrDefault();

                if (campaign == null) return Json(new {state = "Fail", additional = "Unknown Campaign"});

                var newEvent = new Event
                    {
                        Name = group.Name,
                        DateTime = DateTime.Parse(group.Dt),
                        Location = group.Location,
                        Description = group.Description
                    };

                campaign.Events.Add(newEvent);

                _unitOfWork.Save();

                return Json(
                    new
                        {
                            state = "Success",
                            additional = Url.Action("Event", "Event", new {id = newEvent.EventId})
                        });
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
            Campaign campaign = _unitOfWork.CampaignsRepository.FindBy(g => g.CampaignId == Id).FirstOrDefault();

            if (campaign == null) return Json(new {state = "Fail", additional = "Unknown Campaign"});

            var message = new Message {Content = comment, From = _user};

            campaign.Messages.Add(message);

            _unitOfWork.Save();

            return PartialView("_MessagePartial", CreateViewModel<MessageViewModel>().With(message));
        }
    }
}