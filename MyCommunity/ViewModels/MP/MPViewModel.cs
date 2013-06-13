namespace MyCommunity.ViewModels.MP
{
    public class MPViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public MPViewModel With(Models.MP mp)
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
            return this;
        }
    }
}