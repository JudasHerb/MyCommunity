using System;
using System.ComponentModel.DataAnnotations;

namespace MyCommunity.ViewModels.Event
{
    public class CreateEventViewModel
    {

        public CreateEventViewModel()
        {
            
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        [Display(Name="Date")]
        public string Dt { get; set; }

        public string Ctrler { get; set; }

        public int ObjId { get; set; }
    }
}