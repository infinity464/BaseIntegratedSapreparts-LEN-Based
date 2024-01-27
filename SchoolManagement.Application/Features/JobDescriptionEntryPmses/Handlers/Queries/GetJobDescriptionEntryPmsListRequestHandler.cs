using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Handlers.Queries
{
    public class GetJobDescriptionEntryPmsListRequestHandler : IRequestHandler<GetJobDescriptionEntryPmsListRequest, PagedResult<JobDescriptionEntryPmsDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.JobDescriptionEntryPms> _JobDescriptionEntryPmsRepository;

        private readonly IMapper _mapper;

        public GetJobDescriptionEntryPmsListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.JobDescriptionEntryPms> JobDescriptionEntryPmsRepository, IMapper mapper)
        {
            _JobDescriptionEntryPmsRepository = JobDescriptionEntryPmsRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<JobDescriptionEntryPmsDto>> Handle(GetJobDescriptionEntryPmsListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.JobDescriptionEntryPms> JobDescriptionEntryPmss = _JobDescriptionEntryPmsRepository.FilterWithInclude(x => (x.Jic.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText) && x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId), "DepartmentName", "ItemDetail", "MaintenanceType", "MaintenanceCategory", "ByWhom", "SpareRequiredStatusNavigation");
            var totalCount = JobDescriptionEntryPmss.Count();
            JobDescriptionEntryPmss = JobDescriptionEntryPmss.OrderByDescending(x => x.JobDescriptionEntryPmsId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var JobDescriptionEntryPmsDtos = _mapper.Map<List<JobDescriptionEntryPmsDto>>(JobDescriptionEntryPmss);
            var result = new PagedResult<JobDescriptionEntryPmsDto>(JobDescriptionEntryPmsDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
