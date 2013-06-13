using System;
using System.Collections.Generic;
using System.Linq;
using MyCommunity.Models;

namespace MyCommunity.ViewModels.Messaging
{
    public class UserMessagesViewModel : BaseViewModel
    {
        public UserMessagesViewModel With(UserProfile user, UserProfile selectedParticipant)
        {
            Participants = new Dictionary<int, string>();
            foreach (var msg in user.SentMessages)
            {
                if (!Participants.ContainsKey(msg.AddresseeId))
                {
                    Participants.Add(msg.Addressee.UserId,msg.Addressee.DisplayName);
                }
            }
            foreach (var msg in user.ReceivedMessages)
            {
                if (!Participants.ContainsKey(msg.SenderId))
                {
                    Participants.Add(msg.Sender.UserId, msg.Sender.DisplayName);
                }
            }
            var tempList = new List<KeyValuePair<int, string>>(Participants);
            foreach(var kvp in tempList)
            {
                var unread = user.ReceivedMessages.Count(m => m.SenderId == kvp.Key && !m.IsRead);
                if (unread > 0)
                {
                    Participants[kvp.Key] = string.Format("{0} ({1})", kvp.Value, unread.ToString());
                }
            }
            Messages = new List<UserMessageViewModel>();
            if (selectedParticipant != null)
            {

                Messages =
                    user.SentMessages.Where(m => m.AddresseeId == selectedParticipant.UserId)
                        .Select(m => new UserMessageViewModel(m, true))
                        .Union(
                            user.ReceivedMessages.Where(m => m.SenderId == selectedParticipant.UserId)
                                .Select(m => new UserMessageViewModel(m, false))).OrderBy(m => m.Sent).ToList();

                SelectedParticipantId = selectedParticipant.UserId;
            }
            
            return this;
        }
        public int SelectedParticipantId { get; set; }
        public Dictionary<int, string> Participants { get; set; }
        public List<UserMessageViewModel> Messages { get; set; }
        
    }

    public class UserMessageViewModel
    {
        public string Sent { get; set; }
        public string Content { get; set; }
        public bool Sender { get; set; }
        public string By { get; set; }
        public UserMessageViewModel(UserMessage message, bool sender)
        {
            Sent = message.Created.ToString("dd/MM/yy H:mm");
            Content = message.Content;
            Sender = sender;
            By = message.Sender.FirstName;
        }
    }
}