using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences;
using SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Queries;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Handlers.Queries
{
    public class GetJobDescriptionScheduleMaintenenceListRequestHandler : IRequestHandler<GetJobDescriptionScheduleMaintenenceListRequest, PagedResult<JobDescriptionScheduleMaintenenceDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.JobDescriptionScheduleMaintenence> _JobDescriptionScheduleMaintenenceRepository;

        private readonly IMapper _mapper;

        public GetJobDescriptionScheduleMaintenenceListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenenceRepository, IMapper mapper)
        {
            _JobDescriptionScheduleMaintenenceRepository = JobDescriptionScheduleMaintenenceRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<JobDescriptionScheduleMaintenenceDto>> Handle(GetJobDescriptionScheduleMaintenenceListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenences = _JobDescriptionScheduleMaintenenceRepository.FilterWithInclude(x => (String.IsNullOrEmpty(request.QueryParams.SearchText)), "ScheduleMaintenenceType", "JobCategoryMaintenence", "ItemDetail").Where(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemDetailId == request.ItemDetailId);
            var totalCount = JobDescriptionScheduleMaintenences.Count();
            JobDescriptionScheduleMaintenences = JobDescriptionScheduleMaintenences.OrderByDescending(x => x.JobDescriptionScheduleMaintenenceId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var JobDescriptionScheduleMaintenenceDtos = _mapper.Map<List<JobDescriptionScheduleMaintenenceDto>>(JobDescriptionScheduleMaintenences);
            var result = new PagedResult<JobDescriptionScheduleMaintenenceDto>(JobDescriptionScheduleMaintenenceDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
