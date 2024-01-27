using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Handlers.Queries
{
    public class GetSelectedJobDescriptionForScheduleByParameterRequestHandler : IRequestHandler<GetSelectedJobDescriptionForScheduleByParameterRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<JobDescriptionScheduleMaintenence> _JobDescriptionScheduleMaintenenceRepository;

        public GetSelectedJobDescriptionForScheduleByParameterRequestHandler(ISchoolManagementRepository<JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenenceRepository)
        {
            _JobDescriptionScheduleMaintenenceRepository = JobDescriptionScheduleMaintenenceRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedJobDescriptionForScheduleByParameterRequest request, CancellationToken cancellationToken)
        {
            ICollection<JobDescriptionScheduleMaintenence> codeValues = await _JobDescriptionScheduleMaintenenceRepository.FilterAsync(x => x.IsActive && x.UnitId ==request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.IssueRegisterId ==request.IssueRegisterId && x.ScheduleMaintenenceTypeId ==request.ScheduleMaintenenceTypeId && x.FiscalYearId == request.FiscalYearId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.JobDescription,
                Value = x.JobDescriptionScheduleMaintenenceId
            }).ToList();
            return selectModels;
        }
    }
}
