namespace RestaurantWebDomain
{
    public class Drink : BaseEntity
    {
        public string Name { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}
