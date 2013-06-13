using System.Linq;
using System.Web.Mvc;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using MyCommunity.ViewModels.Messaging;

namespace MyCommunity.Controllers
{
    public class MessagingController : BaseController
    {
        private readonly UserProfile _user;

        public MessagingController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _user = _unitOfWork.UsersRepository.CurrentUser();
        }

        public ActionResult Messages(int id)
        {
            var participant = _unitOfWork.UsersRepository.FindBy(u => u.UserId == id).FirstOrDefault();
            if (participant != null)
            {

                foreach (UserMessage msg in _user.ReceivedMessages.Where(m => !m.IsRead&&m.SenderId==participant.UserId))
                {
                    msg.IsRead = true;
                }

                _unitOfWork.Save();
            }

            return View(CreateViewModel<UserMessagesViewModel>().With(_user, participant));
        }
    }
}