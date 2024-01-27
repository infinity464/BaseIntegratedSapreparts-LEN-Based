using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ItemDetails.Requests.Queries;
using SchoolManagement.Application.Features.TraineeBioDataGeneralInfos.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ItemDetails.Handlers.Queries
{
    public class GetAutoCompleteImcForSparesRequestHandler : IRequestHandler<GetAutoCompleteImcForSparesRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ItemDetail> _ItemDetailRepository; 
        public GetAutoCompleteImcForSparesRequestHandler(ISchoolManagementRepository<ItemDetail> ItemDetailRepository)
        {
            _ItemDetailRepository = ItemDetailRepository;
        }
          
        public async Task<List<SelectedModel>> Handle(GetAutoCompleteImcForSparesRequest request, CancellationToken cancellationToken)
        {
            ICollection<ItemDetail> partno = await _ItemDetailRepository.FilterAsync(x => x.IsActive && x.UnitId == request.UnitId && x.DepartmentNameId==request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId && x.ImcNumber.Contains(request.Imc));
            var selectModels = partno.Select(x => new SelectedModel
            { 
                Text = x.ImcNumber,
                Value = x.ItemDetailId
            }).ToList();
            return selectModels;
        }
      }
}
