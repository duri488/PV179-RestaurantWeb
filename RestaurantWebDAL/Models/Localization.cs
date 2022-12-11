using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestaurantWeb.Contract;

namespace RestaurantWebDAL.Models
{
    public class Localization : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(2)]
        [MinLength(2)]
        public string IsoLanguageCode { get; set; }

        [MaxLength(255)]
        [MinLength(1)]
        public string StringCode { get; set; }

        [MaxLength(1000)]
        public string LocalizedString { get; set; }
    }
}
