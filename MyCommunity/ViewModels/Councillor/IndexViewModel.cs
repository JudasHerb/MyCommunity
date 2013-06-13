using System.Collections.Generic;

namespace MyCommunity.ViewModels.Councillor
{
    public class IndexViewModel : BaseViewModel
    {
        public List<CouncillorViewModel> Councillors { get; set; }

        public IndexViewModel With(IEnumerable<Models.Councillor> councillors)
        {
            Councillors = new List<CouncillorViewModel>();
            foreach (Models.Councillor councillor in councillors)
            {
                Councillors.Add(new CouncillorViewModel {Name = councillor.Name, Email = councillor.Email});
            }
            return this;
        }
    }
}