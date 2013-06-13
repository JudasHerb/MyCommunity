using System;
using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.ViewModels;
using MyCommunity.ViewModels.Community;
using MyCommunity.ViewModels.Event;

namespace MyCommunity.Controllers
{
    public class CommunityController : BaseController
    {
        private readonly UserProfile _user;

        public CommunityController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _user = _unitOfWork.UsersRepository.CurrentUser();
        }

        public ActionResult Index()
        {
            return View(CreateViewModel<IndexViewModel>().With(_user));
        }

        public ActionResult MoreComments(int count, int Id)
        {
            if ((4 + count) < _user.Community.Messages.Count)
            {
                Message nextMessage = _user.Community.Messages.Reverse().Skip(4 + count).FirstOrDefault();
                if (nextMessage != null)
                {
                    return PartialView("_MessagePartial", CreateViewModel<MessageViewModel>().With(nextMessage));
                }
            }

            return Json(null);
        }

        [HttpPost]
        public ActionResult Comment(string comment, int Id)
        {
            var message = new Message {Content = comment, From = _user};

            _user.Community.Messages.Add(message);

            _unitOfWork.Save();

            return PartialView("_MessagePartial", CreateViewModel<MessageViewModel>().With(message));
        }

        [HttpPost]
        public ActionResult Event(CreateEventViewModel group)
        {
            if (ModelState.IsValid)
            {
                var newEvent = new Event
                    {
                        Name = group.Name,
                        DateTime = DateTime.Parse(group.Dt),
                        Location = group.Location,
                        Description = group.Description
                    };

                _user.Community.Events.Add(newEvent);

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

        public ActionResult Events()
        {
            return View(CreateViewModel<BrowseEventsViewModel>().With(_user.Community.Events, _user.CommunityID));
        }
    }
}