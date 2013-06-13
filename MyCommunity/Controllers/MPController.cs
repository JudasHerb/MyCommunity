using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.ViewModels.MP;

namespace MyCommunity.Controllers
{
    public class MPController : BaseController
    {
        private readonly UserProfile _user;

        public MPController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _user = _unitOfWork.UsersRepository.CurrentUser();
        }

        public ActionResult MP()
        {
            return View(CreateViewModel<MPViewModel>().With(_user.Community.MP));
        }
    }
}