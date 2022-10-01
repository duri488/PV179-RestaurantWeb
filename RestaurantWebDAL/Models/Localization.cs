using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebDAL.Models
{
    public class Localization : BaseEntity
    {
        public string IsoLanguageCode { get; set; }
        public string StringCode { get; set; }
        public string LocalizedString { get; set; }
    }
}
