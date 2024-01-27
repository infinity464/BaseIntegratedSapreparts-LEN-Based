using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.StatusOfJob;
using SchoolManagement.Application.Features.StatusOfJobs.Requests.Queries;

namespace SchoolManagement.Application.Features.StatusOfJobs.Handlers.Queries
{
    public class GetStatusOfJobListRequestHandler : IRequestHandler<GetStatusOfJobListRequest, PagedResult<StatusOfJobDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.StatusOfJob> _StatusOfJobRepository;

        private readonly IMapper _mapper;

        public GetStatusOfJobListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.StatusOfJob> StatusOfJobRepository, IMapper mapper)
        {
            _StatusOfJobRepository = StatusOfJobRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<StatusOfJobDto>> Handle(GetStatusOfJobListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.StatusOfJob> StatusOfJobs = _StatusOfJobRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = StatusOfJobs.Count();
            StatusOfJobs = StatusOfJobs.OrderByDescending(x => x.StatusOfJobId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var StatusOfJobDtos = _mapper.Map<List<StatusOfJobDto>>(StatusOfJobs);
            var result = new PagedResult<StatusOfJobDto>(StatusOfJobDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
