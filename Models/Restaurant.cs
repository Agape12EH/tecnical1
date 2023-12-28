namespace tecnical1.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
