using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ScheduleMaintenances;
using SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Queries;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Handlers.Queries
{
    public class GetScheduleMaintenanceListByDeptRequestHandler : IRequestHandler<GetScheduleMaintenanceListByDeptRequest, PagedResult<ScheduleMaintenanceDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.ScheduleMaintenance> _ScheduleMaintenanceRepository;

        private readonly IMapper _mapper;

        public GetScheduleMaintenanceListByDeptRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.ScheduleMaintenance> ScheduleMaintenanceRepository, IMapper mapper)
        {
            _ScheduleMaintenanceRepository = ScheduleMaintenanceRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ScheduleMaintenanceDto>> Handle(GetScheduleMaintenanceListByDeptRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.ScheduleMaintenance> ScheduleMaintenances = _ScheduleMaintenanceRepository.FilterWithInclude(x => (String.IsNullOrEmpty(request.QueryParams.SearchText)), "JobDescriptionScheduleMaintenence", "Progres", "FiscalYear", "ByWhom");
            var totalCount = ScheduleMaintenances.Count();
            ScheduleMaintenances = ScheduleMaintenances.OrderBy(x => x.CompletedStatus).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize).Where(x=>x.UnitId ==request.UnitId && x.DepartmentNameId == request.DepartmentNameId);

            var ScheduleMaintenanceDtos = _mapper.Map<List<ScheduleMaintenanceDto>>(ScheduleMaintenances);
            var result = new PagedResult<ScheduleMaintenanceDto>(ScheduleMaintenanceDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
