using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.EquipmentNames.Requests.Queries;
using SchoolManagement.Application.Features.ItemDetails.Requests.Queries;
using SchoolManagement.Application.Features.TraineeBioDataGeneralInfos.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.EquipmentNames.Handlers.Queries
{
    public class GetAutoCompleteEquipmentNameRequestHandler : IRequestHandler<GetAutoCompleteEquipmentNameRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<EquipmentName> _EquipmentNameRepository; 
        public GetAutoCompleteEquipmentNameRequestHandler(ISchoolManagementRepository<EquipmentName> EquipmentNameRepository)
        {
            _EquipmentNameRepository = EquipmentNameRepository;
        }
          
        public async Task<List<SelectedModel>> Handle(GetAutoCompleteEquipmentNameRequest request, CancellationToken cancellationToken)
        {
            ICollection<EquipmentName> partno = await _EquipmentNameRepository.FilterAsync(x => x.Name.Contains(request.Name));
            var selectModels = partno.Select(x => new SelectedModel
            { 
                Text = x.Name,
                Value = x.EquipmentNameId
            }).ToList();
            return selectModels;
        }
      }
}
