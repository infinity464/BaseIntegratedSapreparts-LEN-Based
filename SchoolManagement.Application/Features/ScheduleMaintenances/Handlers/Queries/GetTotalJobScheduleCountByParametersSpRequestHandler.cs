using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Handlers.Queries
{
    public class GetTotalJobScheduleCountByParametersSpRequestHandler : IRequestHandler<GetTotalJobScheduleCountByParametersSpRequest, object>
    {

        private readonly ISchoolManagementRepository<Demand> _studentInfoByTraineeIdRepository;

        private readonly IMapper _mapper;

        public GetTotalJobScheduleCountByParametersSpRequestHandler(ISchoolManagementRepository<Demand> studentInfoByTraineeIdRepository, IMapper mapper)
        {
            _studentInfoByTraineeIdRepository = studentInfoByTraineeIdRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetTotalJobScheduleCountByParametersSpRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [spFindCompletedJobCountByParameters] {0},{1},{2},{3},{4}", request.UnitId, request.DepartmentId, request.IssueRegisterId, request.ScheduleMaintenenceTypeId, request.FiscalYearId);

            DataTable dataTable = _studentInfoByTraineeIdRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
