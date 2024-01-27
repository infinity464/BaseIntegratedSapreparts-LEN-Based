using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Queries
{
    public class GetPmsNoByItemDetailIdRequestHandler : IRequestHandler<GetPmsNoByItemDetailIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<PreventiveMaintenance> _PreventiveMaintenanceRepository;

        public GetPmsNoByItemDetailIdRequestHandler(ISchoolManagementRepository<PreventiveMaintenance> PreventiveMaintenanceRepository)
        {
            _PreventiveMaintenanceRepository = PreventiveMaintenanceRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetPmsNoByItemDetailIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<PreventiveMaintenance> codeValues = await _PreventiveMaintenanceRepository.FilterAsync(x => x.IsActive && x.ItemDetailId==request.ItemDetailId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Pmsno,
                Value = x.PreventiveMaintenanceId
            }).ToList();
            return selectModels;
        }
    }
}
