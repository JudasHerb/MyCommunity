using System;
using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.ViewModels;
using MyCommunity.ViewModels.Event;

namespace MyCommunity.Controllers
{
    public class EventController : BaseController
    {
        private readonly UserProfile _user;

        public EventController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _user = _unitOfWork.UsersRepository.CurrentUser();
        }

        public ActionResult Event(int id)
        {
            Event evt = _unitOfWork.EventsRepository.Find(e => e.EventId == id);

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
            Event evt = _unitOfWork.EventsRepository.Find(e => e.EventId == Id);

            if (evt == null) return Json(new {state = "Fail", additional = "Unknown Event"});

            var message = new Message {Content = comment, From = _user};

            evt.Messages.Add(message);

            _unitOfWork.Save();

            return PartialView("_MessagePartial", CreateViewModel<MessageViewModel>().With(message));
        }
    }
}