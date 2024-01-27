using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Handlers.Queries
{
    public class GetSelectedScheduleMaintenanceRequestHandler : IRequestHandler<GetSelectedScheduleMaintenanceRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ScheduleMaintenance> _ScheduleMaintenanceRepository;


        public GetSelectedScheduleMaintenanceRequestHandler(ISchoolManagementRepository<ScheduleMaintenance> ScheduleMaintenanceRepository)
        {
            _ScheduleMaintenanceRepository = ScheduleMaintenanceRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedScheduleMaintenanceRequest request, CancellationToken cancellationToken)
        {
            ICollection<ScheduleMaintenance> codeValues = await _ScheduleMaintenanceRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.ByWhomId,
                Value = x.ScheduleMaintenanceId
            }).ToList();
            return selectModels;
        }
    }
}
