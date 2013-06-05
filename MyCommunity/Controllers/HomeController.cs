using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.ViewModels;
using WebMatrix.WebData;

namespace MyCommunity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public ActionResult Index()
        {
            var user = _unitOfWork.UsersRepository.CurrentUser();
            
            return View(new IndexViewModel(user));
        }

        public ActionResult Messages()
        {
            var user = _unitOfWork.UsersRepository.CurrentUser();

            return View(new MessagesViewModel(user.Messages.ToList()));
        }

        public ActionResult Councillors()
        {
            var user = _unitOfWork.UsersRepository.CurrentUser();

            return View(new CouncillorsViewModel(user.Community.Councillors.ToList()));
        }
        public ActionResult Neighbours()
        {
            var user = _unitOfWork.UsersRepository.CurrentUser();

            return View(new NeighboursViewModel(user.Community.Members.ToList()));
        }
        public ActionResult Neighbour(int id)
        {
            var user = _unitOfWork.UsersRepository.FindBy(u => u.UserId == id).FirstOrDefault();
            if (user != null)
            {
                return View(new NeighbourViewModel{Name=user.UserName, ID=user.UserId});
            }
            else
            {
                return View(new NeighbourViewModel { Name = "Unknown" });
            }

            
        }

        [HttpPost]
        public ActionResult MessageNeighbour(int id, string comment)
        {
            var target = _unitOfWork.UsersRepository.FindBy(u => u.UserId == id).FirstOrDefault();
            if (target != null)
            {
                target.Messages.Add(new Message{Content=comment});
                _unitOfWork.Save();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CommentGroup(int id, string comment)
        {
            var target = _unitOfWork.UsersRepository.CurrentUser().Groups.Where(g => g.GroupID == id).FirstOrDefault();
            
            if (target != null)
            {
                var msg = new Message {Content = comment};
                target.Messages.Add(msg);
                _unitOfWork.Save();

                 return PartialView("_MessagePartial", new MessageViewModel(msg));
            }
            else
            {
                return Json(new { state = "Fail", additional = comment });
            }
            
        }
        [HttpPost]
        public ActionResult CommentEvent(int id, string comment)
        {
            var target = _unitOfWork.EventsRepository.FindBy(e=>e.EventID==id).FirstOrDefault();

            if (target != null)
            {
                var msg = new Message { Content = comment };
                target.Messages.Add(msg);
                _unitOfWork.Save();

                return PartialView("_MessagePartial", new MessageViewModel(msg));
            }
            else
            {
                return Json(new { state = "Fail", additional = comment });
            }

        }
        [HttpPost]
        public ActionResult CommentCampaign(int id, string comment)
        {
            var target = _unitOfWork.UsersRepository.CurrentUser().Campaigns.Where(g => g.CampaignID == id).FirstOrDefault();
            if (target != null)
            {
                var msg = new Message { Content = comment };
                target.Messages.Add(msg);
                _unitOfWork.Save();

                return PartialView("_MessagePartial", new MessageViewModel(msg));
            }
            else
            {
                return Json(new { state = "Fail", additional = comment });
            }

        }

        public ActionResult MP()
        {
            var user = _unitOfWork.UsersRepository.CurrentUser();

            return View(new MPViewModel(user.Community.MP));
        }

        public ActionResult Event(int id)
        {
            var evt =_unitOfWork.EventsRepository.FindBy(e => e.EventID == id).FirstOrDefault();
            if (evt == null) RedirectToAction("Index");
            return View(new EventViewModel(evt));
        }

        public ActionResult Group(int id)
        {
            var user = _unitOfWork.UsersRepository.CurrentUser();
            var group = user.Community.Groups.Where(g => g.GroupID == id).FirstOrDefault();

            if (group == null) RedirectToAction("Index");

            if (!group.Members.Contains(user) && !group.IsPublic )
            {
                return View("UnauthorisedGroup");
            }
            else
            {
                return View(new GroupViewModel(group));
            }
        }
        public ActionResult Campaign(int id)
        {
            var user = _unitOfWork.UsersRepository.CurrentUser();
            var group = user.Community.Campaigns.Where(c => c.CampaignID == id).FirstOrDefault();

            if (group == null) RedirectToAction("Index");

            if (!group.Members.Contains(user))
            {
                return View("UnauthorisedCampaign");
            }
            else
            {
                return View(new CampaignViewModel(group));
            }
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CreateGroupPartial()
        {
            return PartialView("_CreateGroupPartial");
        }
        public ActionResult CreateCampaignPartial()
        {
            return PartialView("_CreateCampaignPartial");
        }
        public ActionResult CreateEventPartial()
        {
            return PartialView("_CreateEventPartial");
        }
        public ActionResult CreateGroupEventPartial(int id)
        {
            return PartialView("_CreateGroupEventPartial", new CreateGroupEventViewModel{GroupID=id});
        }
        public ActionResult CreateCampaignEventPartial(int id)
        {
            return PartialView("_CreateCampaignEventPartial", new CreateCampaignEventViewModel { CampaignID = id });
        }
 
        [HttpPost]
        public ActionResult CreateGroup(CreateGroupViewModel group)
        {
            
            if (ModelState.IsValid)
            {

                var user = _unitOfWork.UsersRepository.CurrentUser();
                
                var newgroup = new Groups
                    {
                        Name = group.Name,
                        Description = group.Description,
                        IsPublic = group.IsPublic
                    };
                newgroup.Members.Add(user);
                user.Community.Groups.Add(newgroup);
                
                _unitOfWork.Save();

                return Json(
                    new
                        {
                            state = "Success",
                            additional = this.Url.Action("Group", "Home", new {id = 1}, this.Request.Url.Scheme)
                        });
            }
            else
            {
                return Json( new {state = "Fail", additional = "Need to fill in all data"});
            }
            
        }
        [HttpPost]
        public ActionResult CreateEvent(CreateEventViewModel group)
        {

            if (ModelState.IsValid)
            {

                var user = _unitOfWork.UsersRepository.CurrentUser();

                var newgroup = new Events
                {
                    Name = group.Name,
                    DateTime = group.Date
                    
                };
                
                user.Community.Events.Add(newgroup);

                _unitOfWork.Save();

                return Json(
                    new
                    {
                        state = "Success",
                        additional = this.Url.Action("Event", "Home", new { id = 1 }, this.Request.Url.Scheme)
                    });
            }
            else
            {
                return Json(new { state = "Fail", additional = "Need to fill in all data" });
            }

        }
        [HttpPost]
        public ActionResult CreateGroupEvent(CreateGroupEventViewModel group)
        {

            if (ModelState.IsValid)
            {

                //var user = _unitOfWork.UsersRepository.CurrentUser();
                var targetgroup = _unitOfWork.GroupsRepository.FindBy(g => g.GroupID == group.GroupID).FirstOrDefault();

                var newgroup = new Events
                {
                    Name = group.Name,
                    DateTime = DateTime.Parse(group.Date)

                };

                targetgroup.Events.Add(newgroup);
                //user.Community.Events.Add(newgroup);

                _unitOfWork.Save();

                return Json(
                    new
                    {
                        state = "Success",
                        additional = this.Url.Action("Event", "Home", new { id = 1 }, this.Request.Url.Scheme)
                    });
            }
            else
            {
                return Json(new { state = "Fail", additional = "Need to fill in all data" });
            }

        }
        [HttpPost]
        public ActionResult CreateCampaignEvent(CreateCampaignEventViewModel group)
        {

            if (ModelState.IsValid)
            {

                //var user = _unitOfWork.UsersRepository.CurrentUser();
                var targetgroup = _unitOfWork.CampaignsRepository.FindBy(g => g.CampaignID == group.CampaignID).FirstOrDefault();

                var newgroup = new Events
                {
                    Name = group.Name,
                    DateTime = DateTime.Parse(group.Date)

                };

                targetgroup.Events.Add(newgroup);
                //user.Community.Events.Add(newgroup);

                _unitOfWork.Save();

                return Json(
                    new
                    {
                        state = "Success",
                        additional = this.Url.Action("Event", "Home", new { id = 1 }, this.Request.Url.Scheme)
                    });
            }
            else
            {
                return Json(new { state = "Fail", additional = "Need to fill in all data" });
            }

        }
        [HttpPost]
        public ActionResult CreateCampaign(CreateCampaignViewModel campaign)
        {

            if (ModelState.IsValid)
            {

                var user = _unitOfWork.UsersRepository.CurrentUser();

                var newcampaign = new Campaigns
                {
                    Name = campaign.Name,
                    Description = campaign.Description,
                    
                };
                newcampaign.Members.Add(user);
                user.Community.Campaigns.Add(newcampaign);

                _unitOfWork.Save();

                return Json(
                    new
                    {
                        state = "Success",
                        additional = this.Url.Action("Campaign", "Home", new { id = 1 }, this.Request.Url.Scheme)
                    });
            }
            else
            {
                return Json(new { state = "Fail", additional = "Need to fill in all data" });
            }

        }

        [HttpPost]
        public ActionResult Comment(string comment)
        {
            if (ModelState.IsValid)
            {
                var user = _unitOfWork.UsersRepository.CurrentUser();
                var msg = new Message {Content = comment, From = user, Title = "Test"};
                user.Community.Messages.Add(msg);
                
                _unitOfWork.Save();

                return PartialView("_MessagePartial", new MessageViewModel(msg));
            }
            else
            {
                return Json(new { state = "Fail", additional = "Need to fill in all data" });
            }
            
        }

        public ActionResult BrowseCampaigns()
        {
            return View(new BrowseCampaignsViewModel(_unitOfWork.UsersRepository.CurrentUser().Community.Campaigns.ToList()));
        }
        public ActionResult BrowseGroups()
        {
            return View(new BrowseGroupsViewModel(_unitOfWork.UsersRepository.CurrentUser().Community.Groups.ToList()));
        }
        public ActionResult BrowseEvents()
        {
            return View(new BrowseEventsViewModel(_unitOfWork.UsersRepository.CurrentUser().Community.Events.ToList()));
        }

        public ActionResult JoinCampaign(int Id)
        {
            var campaign = _unitOfWork.CampaignsRepository.FindBy(c => c.CampaignID == Id).FirstOrDefault();
            if (campaign != null)
            {
                campaign.Members.Add(_unitOfWork.UsersRepository.CurrentUser());
                _unitOfWork.Save();
            }
            return RedirectToAction("Campaign", new {id = Id});
        }
        public ActionResult JoinGroup(int Id)
        {
            var campaign = _unitOfWork.GroupsRepository.FindBy(c => c.GroupID == Id).FirstOrDefault();
            if (campaign != null)
            {
                campaign.Members.Add(_unitOfWork.UsersRepository.CurrentUser());
                _unitOfWork.Save();
            }
            return RedirectToAction("Group", new { id = Id });
        }
    }
}
