using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ItemDetails.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ItemDetails.Handlers.Queries
{
    public class GetSelectedItemNameByParametersRequestHandler : IRequestHandler<GetSelectedItemNameByParametersRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ItemDetail> _ItemDetailRepository;


        public GetSelectedItemNameByParametersRequestHandler(ISchoolManagementRepository<ItemDetail> ItemDetailRepository)
        {
            _ItemDetailRepository = ItemDetailRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedItemNameByParametersRequest request, CancellationToken cancellationToken)
        {
            IQueryable<ItemDetail> ItemDetails = _ItemDetailRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId==request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId && x.EquipmentNameId == request.EquipmentNameId);
            List<SelectedModel> selectModels = ItemDetails.Select(x => new SelectedModel 
            {
                Text = x.NameOfItem,
                Value = x.ItemDetailId
            }).ToList();
            return selectModels;
        }
    }
}
