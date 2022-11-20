using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.DTOs;
using RestaurantWebDAL.Models;
using RestaurantWebBL.Interfaces;

namespace RestaurantWebBL.QueryObjects
{
    public class LocalizationQueryObject :ILocalizationQueryObject
    {
        private IMapper mapper;
        private IQuery<Localization> myQuery;

        public LocalizationQueryObject(IMapper mapper, IQuery<Localization> _localizationQuery)
        {
            this.mapper = mapper;
            myQuery = _localizationQuery;
        }

        public QueryResultDto<LocalizationDto> GetStringWithCode(LocalizationFilterDTOs filter)
        {
            var query = myQuery
                .Where<string>(a => a == filter.IsoLanguageCode, nameof(Localization.IsoLanguageCode));
            var queryString = query.
                Where<string>(a => a == filter.StringCode, nameof(Localization.StringCode));
            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                queryString = queryString.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                queryString = queryString.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return mapper.Map<QueryResultDto<LocalizationDto>>(queryString.Execute());
        }

        public QueryResultDto<LocalizationDto> GetStringWithIso(LocalizationFilterDTOs filter)
        {
            var query = myQuery
                .Where<string>(a => a == filter.IsoLanguageCode, nameof(Localization.IsoLanguageCode));
            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                query = query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return mapper.Map<QueryResultDto<LocalizationDto>>(query.Execute());
        }

    }
}
