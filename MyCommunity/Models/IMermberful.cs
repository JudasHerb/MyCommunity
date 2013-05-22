using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCommunity.Models
{
    public interface IMermberful
    {
         ICollection<UserProfile> Members { get; set; }
    }
}