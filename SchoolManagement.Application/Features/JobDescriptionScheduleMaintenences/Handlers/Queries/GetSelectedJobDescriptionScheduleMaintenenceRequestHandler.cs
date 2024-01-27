using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Handlers.Queries
{
    public class GetSelectedJobDescriptionScheduleMaintenenceRequestHandler : IRequestHandler<GetSelectedJobDescriptionScheduleMaintenenceRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<JobDescriptionScheduleMaintenence> _JobDescriptionScheduleMaintenenceRepository;


        public GetSelectedJobDescriptionScheduleMaintenenceRequestHandler(ISchoolManagementRepository<JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenenceRepository)
        {
            _JobDescriptionScheduleMaintenenceRepository = JobDescriptionScheduleMaintenenceRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedJobDescriptionScheduleMaintenenceRequest request, CancellationToken cancellationToken)
        {
            ICollection<JobDescriptionScheduleMaintenence> codeValues = await _JobDescriptionScheduleMaintenenceRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.EquipmentNameId,
                Value = x.JobDescriptionScheduleMaintenenceId
            }).ToList();
            return selectModels;
        }
    }
}
