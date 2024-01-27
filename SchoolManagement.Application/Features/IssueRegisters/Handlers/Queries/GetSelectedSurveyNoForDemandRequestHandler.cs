using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.IssueRegisters.Handlers.Queries
{
    public class GetSelectedSurveyNoForDemandRequestHandler : IRequestHandler<GetSelectedSurveyNoForDemandRegisterRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<IssueRegister> _IssueRegisterRepository;


        public GetSelectedSurveyNoForDemandRequestHandler(ISchoolManagementRepository<IssueRegister> IssueRegisterRepository)
        {
            _IssueRegisterRepository = IssueRegisterRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSurveyNoForDemandRegisterRequest request, CancellationToken cancellationToken)
        {
            var codeValues = _IssueRegisterRepository.FilterWithInclude(x => x.IsActive && x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.SurveyStatus ==1 & x.DemandCompleteStatus == 0 );
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.SurveyNumber,
                Value = x.IssueRegisterId
            }).ToList();
            return selectModels;
        }
    }
}
