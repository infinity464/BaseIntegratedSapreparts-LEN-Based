using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Queries
{
    public class GetSelectedPreventiveMaintenanceRequestHandler : IRequestHandler<GetSelectedPreventiveMaintenanceRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<PreventiveMaintenance> _PreventiveMaintenanceRepository;


        public GetSelectedPreventiveMaintenanceRequestHandler(ISchoolManagementRepository<PreventiveMaintenance> PreventiveMaintenanceRepository)
        {
            _PreventiveMaintenanceRepository = PreventiveMaintenanceRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPreventiveMaintenanceRequest request, CancellationToken cancellationToken)
        {
            ICollection<PreventiveMaintenance> codeValues = await _PreventiveMaintenanceRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Pmsno,
                Value = x.PreventiveMaintenanceId
            }).ToList();
            return selectModels;
        }
    }
}
