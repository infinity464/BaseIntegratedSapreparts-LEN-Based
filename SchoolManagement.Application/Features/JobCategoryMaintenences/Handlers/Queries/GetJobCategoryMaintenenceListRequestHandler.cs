using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.JobCategoryMaintenences;
using SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Queries;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Handlers.Queries
{
    public class GetJobCategoryMaintenenceListRequestHandler : IRequestHandler<GetJobCategoryMaintenenceListRequest, PagedResult<JobCategoryMaintenenceDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.JobCategoryMaintenence> _JobCategoryMaintenenceRepository;

        private readonly IMapper _mapper;

        public GetJobCategoryMaintenenceListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.JobCategoryMaintenence> JobCategoryMaintenenceRepository, IMapper mapper)
        {
            _JobCategoryMaintenenceRepository = JobCategoryMaintenenceRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<JobCategoryMaintenenceDto>> Handle(GetJobCategoryMaintenenceListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.JobCategoryMaintenence> JobCategoryMaintenences = _JobCategoryMaintenenceRepository.FilterWithInclude(x => (x.CategoryName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = JobCategoryMaintenences.Count();
            JobCategoryMaintenences = JobCategoryMaintenences.OrderByDescending(x => x.JobCategoryMaintenenceId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var JobCategoryMaintenenceDtos = _mapper.Map<List<JobCategoryMaintenenceDto>>(JobCategoryMaintenences);
            var result = new PagedResult<JobCategoryMaintenenceDto>(JobCategoryMaintenenceDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
