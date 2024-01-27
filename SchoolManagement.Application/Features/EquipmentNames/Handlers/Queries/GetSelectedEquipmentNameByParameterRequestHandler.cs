using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.EquipmentNames.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.EquipmentNames.Handlers.Queries
{
    public class GetSelectedEquipmentNameByParameterRequestHandler : IRequestHandler<GetSelectedEquipmentNameByParameterRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<EquipmentName> _EquipmentNameRepository;

          
        public GetSelectedEquipmentNameByParameterRequestHandler(ISchoolManagementRepository<EquipmentName> EquipmentNameRepository)
        {
            _EquipmentNameRepository = EquipmentNameRepository;           
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEquipmentNameByParameterRequest request, CancellationToken cancellationToken)
        {
            ICollection<EquipmentName> EquipmentNames = await _EquipmentNameRepository.FilterAsync(x =>x.UnitId==request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId);
            List<SelectedModel> selectModels = EquipmentNames.Select(x => new SelectedModel
            {
                Text = x.Name, 
                Value = x.EquipmentNameId
            }).ToList();
            return selectModels;
        }
    }
}
