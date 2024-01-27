using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.SurveyItemType;
using SchoolManagement.Application.Features.SurveyItemTypes.Requests.Queries;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Handlers.Queries
{
    public class GetSurveyItemTypeListRequestHandler : IRequestHandler<GetSurveyItemTypeListRequest, PagedResult<SurveyItemTypeDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SurveyItemType> _SurveyItemTypeRepository;

        private readonly IMapper _mapper;

        public GetSurveyItemTypeListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SurveyItemType> SurveyItemTypeRepository, IMapper mapper)
        {
            _SurveyItemTypeRepository = SurveyItemTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<SurveyItemTypeDto>> Handle(GetSurveyItemTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.SurveyItemType> SurveyItemTypes = _SurveyItemTypeRepository.FilterWithInclude(x => (x.SurveyItemTypeName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = SurveyItemTypes.Count();
            SurveyItemTypes = SurveyItemTypes.OrderByDescending(x => x.SurveyItemTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var SurveyItemTypeDtos = _mapper.Map<List<SurveyItemTypeDto>>(SurveyItemTypes);
            var result = new PagedResult<SurveyItemTypeDto>(SurveyItemTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
