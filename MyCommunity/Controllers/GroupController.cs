using System;
using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.ViewModels;
using MyCommunity.ViewModels.Event;
using MyCommunity.ViewModels.Group;

namespace MyCommunity.Controllers
{
    public class GroupController : BaseController
    {
        private readonly UserProfile _user;

        public GroupController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _user = _unitOfWork.UsersRepository.CurrentUser();
        }

        public ActionResult Group(int id)
        {
            Group group = _unitOfWork.GroupsRepository.Find(g => g.GroupId == id);

            if (group == null) RedirectToAction("Index", "Community");

            if (!group.Members.Contains(_user) && !group.IsPublic) return View("PrivateGroup",CreateViewModel<PrivateGroupViewModel>());

            GroupViewModel model = CreateViewModel<GroupViewModel>().With(group);

            if (!group.Members.Contains(_user) && group.IsPublic) model = model.NotSubscribed();

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
                var newgroup = new Group
                    {
                        Name = group.Name,
                        Description = group.Description,
                        IsPublic = group.IsPublic
                    };

                newgroup.Members.Add(_user);

                _user.Community.Groups.Add(newgroup);

                _unitOfWork.Save();

                return Json(
                    new
                        {
                            state = "Success",
                            additional = Url.Action("Group", new {id = newgroup.GroupId})
                        });
            }

            return Json(new {state = "Fail", additional = "Need to fill in all data"});
        }

        public ActionResult Index()
        {
            return View(CreateViewModel<IndexViewModel>().With(_user.Community.Groups,_user));
        }

        public ActionResult JoinGroup(int Id)
        {
            Group group = _unitOfWork.GroupsRepository.Find(c => c.GroupId == Id);

            if (group == null) RedirectToAction("Index", "Community");

            group.Members.Add(_unitOfWork.UsersRepository.CurrentUser());

            _unitOfWork.Save();

            return RedirectToAction("Group", new {id = Id});
        }

        [HttpPost]
        public ActionResult Event(CreateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = _unitOfWork.UsersRepository.CurrentUser();
                Group group = _unitOfWork.GroupsRepository.Find(g => g.GroupId == model.ObjId);

                var newEvent = new Event
                    {
                        Name = model.Name,
                        DateTime = DateTime.Parse(model.Dt),
                        Location = model.Location,
                        Description = model.Description
                    };

                group.Events.Add(newEvent);

                _unitOfWork.Save();

                return Json(
                    new
                        {
                            state = "Success",
                            additional = Url.Action("Event", "Event", new {id = newEvent.EventId})
                        });
            }

            return Json(new {state = "Fail", additional = "Need to fill in all data"});
        }

        public ActionResult MoreComments(int count, int Id)
        {
            Group group = _unitOfWork.GroupsRepository.Find(gr => gr.GroupId == Id);

            if (group == null) return Json(null);

            if ((4 + count) < group.Messages.Count)
            {
                Message nextMessage = group.Messages.Reverse().Skip(4 + count).FirstOrDefault();
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
            Group group = _unitOfWork.GroupsRepository.Find(g => g.GroupId == Id);

            if (group == null) return Json(new {state = "Fail", additional = "Unknown Group"});

            var message = new Message {Content = comment, From = _user};

            group.Messages.Add(message);

            _unitOfWork.Save();

            return PartialView("_MessagePartial", CreateViewModel<MessageViewModel>().With(message));
        }
    }
}