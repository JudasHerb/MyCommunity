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
            var user = _unitOfWork.UsersRepository.FindBy(u => u.UserName == WebSecurity.CurrentUserName).First();
            var groups = user.Community.Groups.Where(g => g.Members.Contains(user)).ToList();
            var campaigns = user.Community.Campaigns.Where(c => c.Members.Contains(user)).ToList();
            
            return View(new IndexViewModel(user.Community, groups, campaigns));
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
    }
}
