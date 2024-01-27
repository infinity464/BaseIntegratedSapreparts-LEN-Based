using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Surveys.Requests.Queries;
using SchoolManagement.Application.DTOs.Survey;

namespace SchoolManagement.Application.Features.Surveys.Handlers.Queries
{
    public class GetSurveyListRequestHandler : IRequestHandler<GetSurveyListRequest, PagedResult<SurveyDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Survey> _SurveyRepository;

        private readonly IMapper _mapper;

        public GetSurveyListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Survey> SurveyRepository, IMapper mapper)
        {
            _SurveyRepository = SurveyRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<SurveyDto>> Handle(GetSurveyListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.Survey> Surveys = _SurveyRepository.FilterWithInclude(x => String.IsNullOrEmpty(request.QueryParams.SearchText));
            var totalCount = Surveys.Count();
            Surveys = Surveys.OrderByDescending(x => x.SurveyId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var SurveyDtos = _mapper.Map<List<SurveyDto>>(Surveys);
            var result = new PagedResult<SurveyDto>(SurveyDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
