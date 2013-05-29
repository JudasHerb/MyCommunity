using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCommunity.Models;

namespace MyCommunity.ViewModels
{
    public class CouncillorsViewModel
    {
        public CouncillorsViewModel(List<Councillors> councillors)
        {
            Councillors = new List<CouncillorViewModel>();
            foreach (var councillor in councillors)
            {
                Councillors.Add(new CouncillorViewModel{Name=councillor.Name, Email = councillor.Email});
            }
        }

        public List<CouncillorViewModel> Councillors { get; set; }
    }

    public class CouncillorViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}