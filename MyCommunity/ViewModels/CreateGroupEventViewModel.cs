using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCommunity.ViewModels
{
    public class CreateGroupEventViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public int GroupID { get; set; }
    }
    
}