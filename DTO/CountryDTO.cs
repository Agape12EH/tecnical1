using tecnical1.Models;

namespace tecnical1.DTO
{
    public class CountryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string isoCode { get; set; }
        public int Population { get; set; }
        public List<RestaurantDTO> Restaurants { get; set; }
        public List<HotelDTO> Hotels { get; set; }
    }
}
