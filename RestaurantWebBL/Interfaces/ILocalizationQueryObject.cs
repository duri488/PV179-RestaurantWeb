using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces
{
    public interface ILocalizationQueryObject
    {
        public QueryResultDto<LocalizationDto> GetStringWithCode(LocalizationFilterDTOs filter);
        public QueryResultDto<LocalizationDto> GetStringWithIso(LocalizationFilterDTOs filter);
    }
}
