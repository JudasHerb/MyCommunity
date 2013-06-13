using System.Collections.Generic;

namespace MyCommunity.ViewModels
{
    public abstract class BaseViewModel
    {
        protected BaseViewModel()
        {
            Path = new List<string>();
        }

        public string UserName { get; set; }
        public string CommunityName { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<string> Path { get; set; }
    }
}