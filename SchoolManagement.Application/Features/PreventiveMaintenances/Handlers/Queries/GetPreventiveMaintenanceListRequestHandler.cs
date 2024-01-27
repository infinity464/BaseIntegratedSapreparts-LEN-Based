using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Queries
{
    public class GetPreventiveMaintenanceListRequestHandler : IRequestHandler<GetPreventiveMaintenanceListRequest, PagedResult<PreventiveMaintenanceDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.PreventiveMaintenance> _PreventiveMaintenanceRepository;

        private readonly IMapper _mapper;

        public GetPreventiveMaintenanceListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.PreventiveMaintenance> PreventiveMaintenanceRepository, IMapper mapper)
        {
            _PreventiveMaintenanceRepository = PreventiveMaintenanceRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PreventiveMaintenanceDto>> Handle(GetPreventiveMaintenanceListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.PreventiveMaintenance> PreventiveMaintenances = _PreventiveMaintenanceRepository.FilterWithInclude(x => (x.Pmsno.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "JobDescriptionEntryPms", "JobDescriptionEntryPms.ByWhom");
            var totalCount = PreventiveMaintenances.Count();
            PreventiveMaintenances = PreventiveMaintenances.OrderByDescending(x => x.CompletedDate).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var PreventiveMaintenanceDtos = _mapper.Map<List<PreventiveMaintenanceDto>>(PreventiveMaintenances);
            var result = new PagedResult<PreventiveMaintenanceDto>(PreventiveMaintenanceDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
