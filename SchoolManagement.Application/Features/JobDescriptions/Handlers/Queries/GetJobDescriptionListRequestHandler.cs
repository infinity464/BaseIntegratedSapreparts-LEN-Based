using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.JobDescription;
using SchoolManagement.Application.Features.JobDescriptions.Requests.Queries;

namespace SchoolManagement.Application.Features.JobDescriptions.Handlers.Queries
{
    public class GetJobDescriptionListRequestHandler : IRequestHandler<GetJobDescriptionListRequest, PagedResult<JobDescriptionDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.JobDescription> _JobDescriptionRepository;

        private readonly IMapper _mapper;

        public GetJobDescriptionListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.JobDescription> JobDescriptionRepository, IMapper mapper)
        {
            _JobDescriptionRepository = JobDescriptionRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<JobDescriptionDto>> Handle(GetJobDescriptionListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.JobDescription> JobDescriptions = _JobDescriptionRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = JobDescriptions.Count();
            JobDescriptions = JobDescriptions.OrderByDescending(x => x.JobDescriptionId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var JobDescriptionDtos = _mapper.Map<List<JobDescriptionDto>>(JobDescriptions);
            var result = new PagedResult<JobDescriptionDto>(JobDescriptionDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
