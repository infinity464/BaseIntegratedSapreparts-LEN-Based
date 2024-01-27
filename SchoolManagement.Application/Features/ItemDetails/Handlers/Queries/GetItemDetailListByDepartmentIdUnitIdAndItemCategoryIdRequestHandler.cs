using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.ItemDetail;
using SchoolManagement.Application.Features.ItemDetails.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Application.DTOs.Demands;

namespace SchoolManagement.Application.Features.ItemDetails.Handlers.Queries
{
    public class GetItemDetailListByDepartmentIdUnitIdAndItemCategoryIdRequestHandler : IRequestHandler<GetItemDetailListByDepartmentIdUnitIdAndItemCategoryIdRequest, PagedResult<ItemDetailDto>>
    {
        private readonly ISchoolManagementRepository<ItemDetail> _ItemDetailRepository;

        private readonly IMapper _mapper;
        public GetItemDetailListByDepartmentIdUnitIdAndItemCategoryIdRequestHandler(ISchoolManagementRepository<ItemDetail> ItemDetailRepository, IMapper mapper)
        {
            _ItemDetailRepository = ItemDetailRepository;
            _mapper = mapper;
        }
         
        public async Task<PagedResult<ItemDetailDto>> Handle(GetItemDetailListByDepartmentIdUnitIdAndItemCategoryIdRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<ItemDetail> ItemDetails = _ItemDetailRepository.FilterWithInclude(x => x.DepartmentNameId == request.DepartmentNameId && x.UnitId==request.UnitId && x.ItemCategoryId==request.ItemCategoryId, "ItemCategory", "EquipmentName", "EquipmentPriority");

            //var ItemDetailDtos = _mapper.Map<List<ItemDetailDto>>(ItemDetails);

            //return ItemDetailDtos;

            IQueryable<ItemDetail> ItemDetails = _ItemDetailRepository.FilterWithInclude(x => (x.PartNo.Contains(request.QueryParams.SearchText) || x.NameOfItem.Contains(request.QueryParams.SearchText) || x.ImcNumber.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)), "ItemCategory", "EquipmentName", "EquipmentPriority").Where(x => x.DepartmentNameId == request.DepartmentNameId && x.UnitId == request.UnitId && x.ItemCategoryId == request.ItemCategoryId);
            var totalCount = ItemDetails.Count();
            ItemDetails = ItemDetails.OrderByDescending(x => x.ItemDetailId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ItemDetailDtos = _mapper.Map<List<ItemDetailDto>>(ItemDetails);
            var result = new PagedResult<ItemDetailDto>(ItemDetailDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }

    }
}
