using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.JobCategory;
using SchoolManagement.Application.Features.JobCategorys.Requests.Queries;

namespace SchoolManagement.Application.Features.JobCategorys.Handlers.Queries
{
    public class GetJobCategoryListRequestHandler : IRequestHandler<GetJobCategoryListRequest, PagedResult<JobCategoryDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.JobCategory> _JobCategoryRepository;

        private readonly IMapper _mapper;

        public GetJobCategoryListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.JobCategory> JobCategoryRepository, IMapper mapper)
        {
            _JobCategoryRepository = JobCategoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<JobCategoryDto>> Handle(GetJobCategoryListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.JobCategory> JobCategorys = _JobCategoryRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = JobCategorys.Count();
            JobCategorys = JobCategorys.OrderByDescending(x => x.JobCategoryId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var JobCategoryDtos = _mapper.Map<List<JobCategoryDto>>(JobCategorys);
            var result = new PagedResult<JobCategoryDto>(JobCategoryDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
