using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Handlers.Queries
{
    public class GetJobDescriptionListByParameterRequestHandler :  IRequestHandler<GetJobDescriptionListByParameterRequest, List<JobDescriptionEntryPmsDto>>
    {
        private readonly ISchoolManagementRepository<JobDescriptionEntryPms> _JobDescriptionEntryPmsRepository;
        private readonly IMapper _mapper;


        public GetJobDescriptionListByParameterRequestHandler(ISchoolManagementRepository<JobDescriptionEntryPms> JobDescriptionEntryPmsRepository, IMapper mapper)
        {
            _JobDescriptionEntryPmsRepository = JobDescriptionEntryPmsRepository;
            _mapper = mapper;
        }

        

        public async Task<List<JobDescriptionEntryPmsDto>> Handle(GetJobDescriptionListByParameterRequest request, CancellationToken cancellationToken)
        {
            IQueryable<JobDescriptionEntryPms> JobDescriptionEntryPmss = _JobDescriptionEntryPmsRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemDetailId ==request.ItemDetailId, "ItemDetail", "MaintenanceType", "MaintenanceCategory", "ByWhom");
            

            var JobDescriptionEntryPmsDtos = _mapper.Map<List<JobDescriptionEntryPmsDto>>(JobDescriptionEntryPmss);

            return JobDescriptionEntryPmsDtos;
        }
    }
}
