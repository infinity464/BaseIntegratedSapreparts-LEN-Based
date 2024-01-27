using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.SurveyItems.Requests.Queries;
using SchoolManagement.Application.DTOs.SurveyItem;

namespace SchoolManagement.Application.Features.SurveyItems.Handlers.Queries
{
    public class GetSurveyItemListRequestHandler : IRequestHandler<GetSurveyItemListRequest, PagedResult<SurveyItemDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SurveyItem> _SurveyItemRepository;

        private readonly IMapper _mapper;

        public GetSurveyItemListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SurveyItem> SurveyItemRepository, IMapper mapper)
        {
            _SurveyItemRepository = SurveyItemRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<SurveyItemDto>> Handle(GetSurveyItemListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.SurveyItem> SurveyItems = _SurveyItemRepository.FilterWithInclude(x => String.IsNullOrEmpty(request.QueryParams.SearchText));
            var totalCount = SurveyItems.Count();
            SurveyItems = SurveyItems.OrderByDescending(x => x.SurveyItemId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var SurveyItemDtos = _mapper.Map<List<SurveyItemDto>>(SurveyItems);
            var result = new PagedResult<SurveyItemDto>(SurveyItemDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
