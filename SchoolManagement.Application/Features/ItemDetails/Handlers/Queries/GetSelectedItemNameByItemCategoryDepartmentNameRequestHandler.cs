using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ItemDetails.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ItemDetails.Handlers.Queries
{
    public class GetSelectedItemNameByItemCategoryDepartmentNameRequestHandler : IRequestHandler<GetSelectedItemNameByItemCategoryDepartmentNameRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ItemDetail> _ItemDetailRepository;


        public GetSelectedItemNameByItemCategoryDepartmentNameRequestHandler(ISchoolManagementRepository<ItemDetail> ItemDetailRepository)
        {
            _ItemDetailRepository = ItemDetailRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedItemNameByItemCategoryDepartmentNameRequest request, CancellationToken cancellationToken)
        {
            IQueryable<ItemDetail> ItemDetails = _ItemDetailRepository.FilterWithInclude(x => x.IsActive && x.DepartmentNameId==request.DepartmentNameId && x.ItemCategoryId ==1);
            List<SelectedModel> selectModels = ItemDetails.Select(x => new SelectedModel 
            {
                Text = x.NameOfItem,
                Value = x.ItemDetailId
            }).ToList();
            return selectModels;
        }
    }
}
