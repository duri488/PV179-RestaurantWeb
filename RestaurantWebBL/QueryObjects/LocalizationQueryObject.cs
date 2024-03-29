﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.QueryObjects
{
    public class LocalizationQueryObject :ILocalizationQueryObject
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider; 

        public LocalizationQueryObject(IMapper mapper, IServiceProvider serviceProvider)
        {
            this._mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public QueryResultDto<LocalizationDto> GetStringWithCode(LocalizationFilterDTOs filter)
        {
            var query = _serviceProvider.GetRequiredService<IQuery<Localization>>();
            query
                .Where<string>(a => a == filter.IsoLanguageCode, nameof(Localization.IsoLanguageCode))
                .Where<string>(a => a == filter.StringCode, nameof(Localization.StringCode));
            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            IEnumerable<Localization> localizations = query.Execute();
            IEnumerable<LocalizationDto> localizationDtos = _mapper.Map<IEnumerable<LocalizationDto>>(localizations).ToList();
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
            var query = _serviceProvider.GetRequiredService<IQuery<Localization>>();
            query
                .Where<string>(a => a == filter.IsoLanguageCode, nameof(Localization.IsoLanguageCode));
            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return _mapper.Map<QueryResultDto<LocalizationDto>>(query.Execute());
        }

    }
}
