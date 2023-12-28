namespace tecnical1.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string isoCode { get; set; }
        public int Population { get; set; }
        public HashSet<Restaurant> Restaurants { get; set; }
        public HashSet<Hotel> Hotels { get; set; }
    }


}
