using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCommunity.Models
{
    public interface IMessagable
    {
        ICollection<Message> Messages { get; set; }
      
    }
}