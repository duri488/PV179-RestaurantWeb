using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

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

            IEnumerable<Localization> localizations = queryString.Execute();
            IEnumerable<LocalizationDto> localizationDtos = mapper.Map<IEnumerable<LocalizationDto>>(localizations).ToList();
            return new QueryResultDto<LocalizationDto>
            {
                Items = localizationDtos,
                PageSize = filter.PageSize,
                RequestedPageNumber = filter.RequestedPageNumber,
                TotalItemsCount = localizationDtos.Count()
            };
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
