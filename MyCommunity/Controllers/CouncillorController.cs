using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.ViewModels.Councillor;

namespace MyCommunity.Controllers
{
    public class CouncillorController : BaseController
    {
        private readonly UserProfile _user;

        public CouncillorController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _user = _unitOfWork.UsersRepository.CurrentUser();
        }

        public ActionResult Index()
        {
            return View(CreateViewModel<IndexViewModel>().With(_user.Community.Councillors));
        }
    }
}