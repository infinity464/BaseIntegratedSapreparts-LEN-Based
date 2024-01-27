using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Handlers.Queries
{
    public class GetSelectedEquipmentPriorityRequestHandler : IRequestHandler<GetSelectedEquipmentPriorityRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<EquipmentPriority> _EquipmentPriorityRepository;


        public GetSelectedEquipmentPriorityRequestHandler(ISchoolManagementRepository<EquipmentPriority> EquipmentPriorityRepository)
        {
            _EquipmentPriorityRepository = EquipmentPriorityRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEquipmentPriorityRequest request, CancellationToken cancellationToken)
        {
            ICollection<EquipmentPriority> codeValues = await _EquipmentPriorityRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.EquipmentPriorityId 
            }).ToList();
            return selectModels;
        }
    }
}
