using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Handlers.Queries
{
    public class GetJobDescriptionEntryPmsListForPreventiveRequestHandler : IRequestHandler<GetJobDescriptionEntryPmsListForPreventiveRequest, List<JobDescriptionEntryPmsDto>>
    {
        private readonly ISchoolManagementRepository<JobDescriptionEntryPms> _JobDescriptionEntryPmsRepository;

        private readonly IMapper _mapper;
        public GetJobDescriptionEntryPmsListForPreventiveRequestHandler(ISchoolManagementRepository<JobDescriptionEntryPms> JobDescriptionEntryPmsRepository, IMapper mapper)
        {
            _JobDescriptionEntryPmsRepository = JobDescriptionEntryPmsRepository;
            _mapper = mapper;
        }

        public async Task<List<JobDescriptionEntryPmsDto>> Handle(GetJobDescriptionEntryPmsListForPreventiveRequest request, CancellationToken cancellationToken)
        {
            IQueryable<JobDescriptionEntryPms> JobDescriptionEntryPmss = _JobDescriptionEntryPmsRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.MaintenanceTypeId==request.MaintenanceTypeId && x.MaintenanceCategoryId ==request.MaintenanceCategoryId && x.ItemDetailId == request.ItemDetailId, "DepartmentName", "ItemDetail", "MaintenanceType", "MaintenanceCategory", "ByWhom");

            var JobDescriptionEntryPmsDtos = _mapper.Map<List<JobDescriptionEntryPmsDto>>(JobDescriptionEntryPmss);

            return JobDescriptionEntryPmsDtos;
        }

    }
}
