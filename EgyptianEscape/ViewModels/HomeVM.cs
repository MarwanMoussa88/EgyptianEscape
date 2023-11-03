using EgyptianEscape.Domain.Models.Villa;

namespace EgyptianEscape.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<GetVilla> Villas { get; set; }
        public int Nights { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
