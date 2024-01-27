using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ItemDetails.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ItemDetails.Handlers.Queries
{
    public class GetEquipmentNameByUnitAndDepartmentAndCategoryIdRequestHandler : IRequestHandler<GetEquipmentNameByUnitAndDepartmentAndCategoryIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ItemDetail> _ItemDetailRepository;
        private readonly ISchoolManagementRepository<EquipmentName> _equipmentNameRepository;


        public GetEquipmentNameByUnitAndDepartmentAndCategoryIdRequestHandler(ISchoolManagementRepository<ItemDetail> ItemDetailRepository, ISchoolManagementRepository<EquipmentName> equipmentNameRepository)
        {
            _ItemDetailRepository = ItemDetailRepository;
            _equipmentNameRepository = equipmentNameRepository; 
        }

        public async Task<List<SelectedModel>> Handle(GetEquipmentNameByUnitAndDepartmentAndCategoryIdRequest request, CancellationToken cancellationToken)
        {

            //var ItemDetails =  _ItemDetailRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId, "EquipmentName");
           
            //List<SelectedModel> selectModels = ItemDetails.Select(x => new SelectedModel
             
            //{
            //    Text = x.EquipmentName.Name,

            //var ItemDetails =  _ItemDetailRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId, "EquipmentName");
            //List<SelectedModel> selectModels = ItemDetails.Select(x => new SelectedModel
            //{
            //    Text = x.EquipmentName.Name,
            //    Value = x.EquipmentNameId
            //}).ToList();
            //return selectModels;


            var ReternModels = new List<SelectedModel>();
            var ItemDetails = _ItemDetailRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId, "EquipmentName").ToList();
            var ItemDetailList = ItemDetails.GroupBy(x => x.EquipmentName.EquipmentNameId).Select(g => g.FirstOrDefault().EquipmentNameId.ToString()).ToList();

            IQueryable<EquipmentName> SubjectMarks = _equipmentNameRepository.FilterWithInclude(x => ItemDetailList.Contains(x.EquipmentNameId.ToString()));
            ReternModels = new List<SelectedModel>();
            ReternModels = SubjectMarks.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.EquipmentNameId
            }).ToList();
            return ReternModels;
        }
    }
    
}
