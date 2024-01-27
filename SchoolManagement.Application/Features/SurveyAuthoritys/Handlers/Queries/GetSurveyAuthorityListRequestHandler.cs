using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.SurveyAuthority;
using SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Queries;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Handlers.Queries
{
    public class GetSurveyAuthorityListRequestHandler : IRequestHandler<GetSurveyAuthorityListRequest, PagedResult<SurveyAuthorityDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SurveyAuthority> _SurveyAuthorityRepository;

        private readonly IMapper _mapper;

        public GetSurveyAuthorityListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SurveyAuthority> SurveyAuthorityRepository, IMapper mapper)
        {
            _SurveyAuthorityRepository = SurveyAuthorityRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<SurveyAuthorityDto>> Handle(GetSurveyAuthorityListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.SurveyAuthority> SurveyAuthoritys = _SurveyAuthorityRepository.FilterWithInclude(x => (x.SurveyAuthorityName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = SurveyAuthoritys.Count();
            SurveyAuthoritys = SurveyAuthoritys.OrderByDescending(x => x.SurveyAuthorityId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var SurveyAuthorityDtos = _mapper.Map<List<SurveyAuthorityDto>>(SurveyAuthoritys);
            var result = new PagedResult<SurveyAuthorityDto>(SurveyAuthorityDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
