using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.IssueRegisters.Handlers.Queries
{
    public class GetSelectedEquipmentNameForSurveyByParameterRequestHandler : IRequestHandler<GetSelectedEquipmentNameForSurveyByParameterRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<IssueRegister> _IssueRegisterRepository;


        public GetSelectedEquipmentNameForSurveyByParameterRequestHandler(ISchoolManagementRepository<IssueRegister> IssueRegisterRepository)
        {
            _IssueRegisterRepository = IssueRegisterRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEquipmentNameForSurveyByParameterRequest request, CancellationToken cancellationToken)
        {
            var codeValues = _IssueRegisterRepository.FilterWithInclude(x => x.IsActive && x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.SurveyStatus == 1 && x.SurveyNumber == null, "EquipmentName", "ToolsLocation");
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.ItemDetail.NameOfItem + " _ " + x.ToolsLocation.ToolsLocationName,
                Value = x.IssueRegisterId
            }).ToList();
            return selectModels;
        }
    }
}
