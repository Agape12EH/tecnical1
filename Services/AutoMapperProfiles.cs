using AutoMapper;
using tecnical1.DTO;
using tecnical1.Models;

namespace tecnical1.Services
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Country, CountryDTO>();
            CreateMap<Hotel, HotelDTO>();
            CreateMap<Restaurant, RestaurantDTO>();
        }
    }
}
