using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebBL.DTOs.FilterDTOs
{
    public class LocalizationFilterDTOs
    {
        public string IsoLanguageCode { get; set; }
        public string StringCode { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortCriteria { get; set; }
        public bool SortAscending { get; set; }
    }
}
