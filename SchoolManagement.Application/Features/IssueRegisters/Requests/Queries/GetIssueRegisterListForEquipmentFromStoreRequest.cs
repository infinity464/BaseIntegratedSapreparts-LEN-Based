using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.DTOs.ItemStor;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Queries
{
    public class GetIssueRegisterListForEquipmentFromStoreRequest : IRequest<List<ItemStorDto>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int ItemCategoryId { get; set; }
        public int ItemDetailId { get; set; }
    }
}   
      