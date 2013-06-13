using System.Collections.Generic;
using System.Linq;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class MessagesViewModel : BaseViewModel
    {
        public List<MessageViewModel> Messages { get; set; }
        public string Controller { get; set; }
        public int ObjId { get; set; }
        public bool ShowForm { get; set; }
        public bool ShowMore { get; set; }

        public MessagesViewModel With(IEnumerable<Message> messages)
        {
            Messages = new List<MessageViewModel>();
            foreach (Message msg in messages)
            {
                Messages.Add(new MessageViewModel().With(msg));
            }
            if (messages.Count() > 5) ShowMore = true;
            return this;
        }

        public MessagesViewModel Using(string controller, int objId)
        {
            Controller = controller;
            ObjId = objId;
            return this;
        }

        public MessagesViewModel ShowPostForm()
        {
            ShowForm = true;
            return this;
        }
    }

    public class MessageViewModel : BaseViewModel
    {
        public string Message { get; set; }
        public string From { get; set; }
        public int FromId { get; set; }

        public MessageViewModel With(Message msg)
        {
            Message = msg.Content;
            if (msg.From != null)
            {
                From = msg.From.DisplayName;
                FromId = msg.From.UserId;
            }
            return this;
        }
    }
}