using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetDemandsByParametersSpRequestHandler : IRequestHandler<GetDemandsByParametersSpRequest, object>
    {

        private readonly ISchoolManagementRepository<Demand> _studentInfoByTraineeIdRepository;

        private readonly IMapper _mapper;

        public GetDemandsByParametersSpRequestHandler(ISchoolManagementRepository<Demand> studentInfoByTraineeIdRepository, IMapper mapper)
        {
            _studentInfoByTraineeIdRepository = studentInfoByTraineeIdRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDemandsByParametersSpRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [spFindDemandByParameters] {0},{1},{2},{3},{4}", request.UnitId, request.DepartmentId, request.ItemCategoryId, request.EquipmentNameId, request.ItemNameId);

            DataTable dataTable = _studentInfoByTraineeIdRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
