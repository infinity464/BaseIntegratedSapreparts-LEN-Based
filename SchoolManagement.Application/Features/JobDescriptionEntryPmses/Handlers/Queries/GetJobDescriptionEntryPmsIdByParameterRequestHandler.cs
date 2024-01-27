using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Handlers.Queries
{
    public class GetJobDescriptionEntryPmsIdByParameterRequestHandler : IRequestHandler<GetJobDescriptionEntryPmsIdByParameterRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<JobDescriptionEntryPms> _JobDescriptionEntryPmsRepository;

          
        public GetJobDescriptionEntryPmsIdByParameterRequestHandler(ISchoolManagementRepository<JobDescriptionEntryPms> JobDescriptionEntryPmsRepository)
        {
            _JobDescriptionEntryPmsRepository = JobDescriptionEntryPmsRepository;           
        }

        public async Task<List<SelectedModel>> Handle(GetJobDescriptionEntryPmsIdByParameterRequest request, CancellationToken cancellationToken)
        {
            ICollection<JobDescriptionEntryPms> JobDescriptionEntryPmss = await _JobDescriptionEntryPmsRepository.FilterAsync(x =>x.UnitId == request.UnitId && x.DepartmentNameId==request.DepartmentNameId && x.MaintenanceTypeId==request.MaintenanceTypeId && x.MaintenanceCategoryId == request.MaintenanceCategoryId && x.ItemDetailId==request.ItemDetailId);
            List<SelectedModel> selectModels = JobDescriptionEntryPmss.Select(x => new SelectedModel
            {
                Text = x.Jic, 
                Value = x.JobDescriptionEntryPmsId 
            }).ToList();
            return selectModels;
        }
    }
}
