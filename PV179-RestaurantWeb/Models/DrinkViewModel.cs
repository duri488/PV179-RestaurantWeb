namespace PV179_RestaurantWeb.Models
{
    public class DrinkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<AllergenViewModel> Allergens { get; set; }
    }
}
