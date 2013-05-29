using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class MPViewModel
    {
        public MPViewModel(MP mp)
        {
            if (mp == null)
            {
                Name = "unknown";
                Email = "unknown";
            }
            else
            {
                Name = mp.Name;
                Email = mp.Email;
            }
            
        }

        public string Name { get; set; }
        public string Email { get; set; }
    }
}