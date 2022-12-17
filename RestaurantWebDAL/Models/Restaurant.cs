using System.ComponentModel.DataAnnotations;
using RestaurantWeb.Contract;

namespace RestaurantWebDAL.Models
{
    public class Restaurant : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        [MinLength(2)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        [MaxLength(255)]
        public string Phone { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
    }
}
