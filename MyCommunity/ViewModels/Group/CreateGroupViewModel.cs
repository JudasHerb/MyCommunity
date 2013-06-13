using System.ComponentModel.DataAnnotations;

namespace MyCommunity.ViewModels.Group
{
    public class CreateGroupViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Is Public")]
        public bool IsPublic { get; set; }
    }
}