using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Handlers.Queries
{
    public class GetSelectedMaintenanceAuthorityRequestHandler : IRequestHandler<GetSelectedMaintenanceAuthorityRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<MaintenanceAuthority> _MaintenanceAuthorityRepository;


        public GetSelectedMaintenanceAuthorityRequestHandler(ISchoolManagementRepository<MaintenanceAuthority> MaintenanceAuthorityRepository)
        {
            _MaintenanceAuthorityRepository = MaintenanceAuthorityRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedMaintenanceAuthorityRequest request, CancellationToken cancellationToken)
        {
            ICollection<MaintenanceAuthority> codeValues = await _MaintenanceAuthorityRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.MaintenanceAuthorityId
            }).ToList();
            return selectModels;
        }
    }
}
