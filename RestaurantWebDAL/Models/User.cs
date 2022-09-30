using System.ComponentModel.DataAnnotations;

namespace RestaurantWebDAL.Models
{
    public class User : BaseEntity
    {
        [MaxLength(255)]
        [MinLength(6)]
        [Key]
        public string Username { get; set; }
        [MaxLength(256/8)]
        [MinLength(256/8)]
        public byte[] HashedPassword { get; set; }
        [MaxLength(128/8)]
        [MinLength(128/8)]
        public byte[] Salt { get; set; }
    }
}
