using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class MessagesViewModel
    {
        public MessagesViewModel(IList<Message> messages)
        {
            Messages = new List<MessageViewModel>();
            foreach (var msg in messages.Take(5))
            {
                Messages.Add( new MessageViewModel(msg));
            }
        }

        public List<MessageViewModel> Messages { get; set; }
    }

    public class MessageViewModel
    {
        public MessageViewModel(Message msg)
        {
            Message = msg.Content;
            if (msg.From != null)
            {
                From = msg.From.DisplayName;
            }
        }

        public string Message { get; set; }
        public string From { get; set; }
    }
}