using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.ViewModels.Messaging;
using MyCommunity.ViewModels.Neighbour;

namespace MyCommunity.Controllers
{
    public class NeighbourController : BaseController
    {
        private readonly UserProfile _user;

        public NeighbourController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _user = _unitOfWork.UsersRepository.CurrentUser();
        }


        public ActionResult Index()
        {
            return View(CreateViewModel<IndexViewModel>().With(_user.Community.Members));
        }

        public ActionResult Neighbour(int id)
        {
            UserProfile neighbour = _unitOfWork.UsersRepository.FindBy(u => u.UserId == id).FirstOrDefault();

            if (neighbour == null) return RedirectToAction("Index", "Community");

            return View(CreateViewModel<NeighbourViewModel>().With(neighbour));
        }

        [HttpPost]
        public ActionResult Message(MessageNeighbourViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile neighbour =
                    _unitOfWork.UsersRepository.FindBy(u => u.UserId == model.AddresseeId).FirstOrDefault();

                if (neighbour == null) return Json(null);

                var message = new UserMessage
                    {
                        Content = model.Comment,
                        Sender = _user,
                        Addressee = neighbour
                    };

                _unitOfWork.UserMessageRepository.Add(message);

                _unitOfWork.Save();

                return PartialView("_UserMessagePartial", new UserMessageViewModel(message,true));
            }

            return Json(null);
        }
    }
}