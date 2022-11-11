using Optional;

namespace RestaurantWebDomain
{
    public class WeeklyMenu : BaseEntity
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Option<int> MealId { get; set; }
        public Meal Meal { get; set; }
        public Option<int> RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
