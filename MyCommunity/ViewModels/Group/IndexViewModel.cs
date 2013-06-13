using System.Collections.Generic;
using MyCommunity.Models;

namespace MyCommunity.ViewModels.Group
{
    public class IndexViewModel : BaseViewModel
    {
        public List<GroupViewModel> Groups { get; set; }

        public IndexViewModel With(IEnumerable<Models.Group> campaigns, UserProfile user)
        {
            Groups = new List<GroupViewModel>();
            foreach (Models.Group grp in campaigns)
            {
                var grpModel = new GroupViewModel().With(grp);
                if (!grp.Members.Contains(user)) grpModel.NotSubscribed();
                Groups.Add(grpModel);
            }
            return this;
        }
    }
}