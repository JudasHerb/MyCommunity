using System.ComponentModel.DataAnnotations;

namespace MyCommunity.ViewModels.Messaging
{
    public class MessageNeighbourViewModel
    {
        [Required]
        public int AddresseeId { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}