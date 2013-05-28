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
            
            return View(new IndexViewModel(user.Community));
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
                return View();
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
        public ActionResult Comment(string comment)
        {
            if (ModelState.IsValid)
            {
                var user = _unitOfWork.UsersRepository.CurrentUser();

                user.Community.Messages.Add(new Message {Content = comment, From = user, Title = "Test"});
                
                _unitOfWork.Save();

                return Json(new { state = "Success", additional = comment });
            }
            else
            {
                return Json(new { state = "Fail", additional = "Need to fill in all data" });
            }
            
        }
    }
}
