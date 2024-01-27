using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Queries
{
    public class GetPreventiveListForMonitorintByParametersSpRequestHandler : IRequestHandler<GetPreventiveListForMonitorintByParametersSpRequest, object>
    {

        private readonly ISchoolManagementRepository<PreventiveMaintenance> _studentInfoByTraineeIdRepository;

        private readonly IMapper _mapper;

        public GetPreventiveListForMonitorintByParametersSpRequestHandler(ISchoolManagementRepository<PreventiveMaintenance> studentInfoByTraineeIdRepository, IMapper mapper)
        {
            _studentInfoByTraineeIdRepository = studentInfoByTraineeIdRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetPreventiveListForMonitorintByParametersSpRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [spFindPreventiveByParameters] {0},{1},{2},{3},{4}", request.UnitId, request.DepartmentId, request.ItemDetailId,request.MaintenanceCategoryId,request.MaintenenceTypeId);

            DataTable dataTable = _studentInfoByTraineeIdRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
