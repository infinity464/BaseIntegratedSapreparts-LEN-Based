using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetProcurementByParametersSpRequestHandler : IRequestHandler<GetProcurementByParametersSpRequest, object>
    {

        private readonly ISchoolManagementRepository<Demand> _studentInfoByTraineeIdRepository;

        private readonly IMapper _mapper;

        public GetProcurementByParametersSpRequestHandler(ISchoolManagementRepository<Demand> studentInfoByTraineeIdRepository, IMapper mapper)
        {
            _studentInfoByTraineeIdRepository = studentInfoByTraineeIdRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetProcurementByParametersSpRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [spFindProcurementByParameters] {0},{1},{2},{3},{4}", request.UnitId, request.DepartmentId, request.ItemCategoryId, request.EquipmentNameId, request.DemandId);

            DataTable dataTable = _studentInfoByTraineeIdRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
