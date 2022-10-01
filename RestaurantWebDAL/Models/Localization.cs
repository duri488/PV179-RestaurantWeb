using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebDAL.Models
{
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
