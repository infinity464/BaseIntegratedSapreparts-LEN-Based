using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;
using SchoolManagement.Application.Features.Surveys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Surveys.Handlers.Queries
{
    public class GetSelectSurveyByUnitAndDepartmentIdRequestHandler : IRequestHandler<GetSelectSurveyByUnitAndDepartmentIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Survey> _SurveyRepository;


        public GetSelectSurveyByUnitAndDepartmentIdRequestHandler(ISchoolManagementRepository<Survey> SurveyRepository)
        {
            _SurveyRepository = SurveyRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectSurveyByUnitAndDepartmentIdRequest request, CancellationToken cancellationToken)
        {
            var Surveys =  _SurveyRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId ==request.ItemCategoryId && x.DemandCompleteStatus==0);
            List<SelectedModel> selectModels = Surveys.Select(x => new SelectedModel
            {
                Text = x.SurveyNumber,
                Value = x.SurveyId
            }).ToList();
            return selectModels;
        }
    }
    
}
