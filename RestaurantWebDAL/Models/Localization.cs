using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantWebDAL.Models
{
    [Index(nameof(StringCode),IsUnique = true)]
    public class Localization : BaseEntity
    {
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
