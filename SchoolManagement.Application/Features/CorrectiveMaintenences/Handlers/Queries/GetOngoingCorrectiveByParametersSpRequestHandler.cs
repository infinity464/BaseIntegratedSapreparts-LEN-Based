using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Handlers.Queries
{
    public class GetOngoingCorrectiveByParametersSpRequestHandler : IRequestHandler<GetOngoingCorrectiveByParametersSpRequest, object>
    {

        private readonly ISchoolManagementRepository<Demand> _studentInfoByTraineeIdRepository;

        private readonly IMapper _mapper;

        public GetOngoingCorrectiveByParametersSpRequestHandler(ISchoolManagementRepository<Demand> studentInfoByTraineeIdRepository, IMapper mapper)
        {
            _studentInfoByTraineeIdRepository = studentInfoByTraineeIdRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetOngoingCorrectiveByParametersSpRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [spFindCorrectiveOngoingCountByParameters] {0},{1},{2},{3},{4}", request.UnitId, request.DepartmentId, request.ItemCategoryId, request.EquipmentNameId, request.ItemNameId);

            DataTable dataTable = _studentInfoByTraineeIdRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
