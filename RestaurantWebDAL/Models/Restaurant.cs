using System.ComponentModel.DataAnnotations;

namespace RestaurantWebDAL.Models
{
    public class Restaurant : BaseEntity
    {
        [MaxLength(255)]
        [MinLength(2)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        [MaxLength(255)]
        public string Phone { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
    }
}
