using System.ComponentModel.DataAnnotations;

namespace PV179_RestaurantWeb.Models
{
    public class RestaurantViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string ContactMail { get; set; }
        [Required]
        public string ContactMessage { get; set; }
    }
}
