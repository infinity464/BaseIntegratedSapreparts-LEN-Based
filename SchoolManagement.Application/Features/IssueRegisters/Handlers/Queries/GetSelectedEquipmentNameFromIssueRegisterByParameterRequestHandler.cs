using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.IssueRegisters.Handlers.Queries
{
    public class GetSelectedEquipmentNameFromIssueRegisterByParameterRequestHandler : IRequestHandler<GetSelectedEquipmentNameFromIssueRegisterByParameterRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<IssueRegister> _IssueRegisterRepository;
         

        public GetSelectedEquipmentNameFromIssueRegisterByParameterRequestHandler(ISchoolManagementRepository<IssueRegister> IssueRegisterRepository)
        {
            _IssueRegisterRepository = IssueRegisterRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEquipmentNameFromIssueRegisterByParameterRequest request, CancellationToken cancellationToken)
        {
            var codeValues = _IssueRegisterRepository.FilterWithInclude(x => x.IsActive && x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId, "EquipmentName", "ToolsLocation");
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.ItemDetail.NameOfItem+" _ "+x.ToolsLocation.ToolsLocationName,
                Value = x.IssueRegisterId
            }).ToList();
            return selectModels;
        }
    }
}
