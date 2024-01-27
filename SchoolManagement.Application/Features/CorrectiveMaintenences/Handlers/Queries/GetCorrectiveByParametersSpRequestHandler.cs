using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Handlers.Queries
{
    public class GetCorrectiveByParametersSpRequestHandler : IRequestHandler<GetCorrectiveByParametersSpRequest, object>
    {

        private readonly ISchoolManagementRepository<Demand> _studentInfoByTraineeIdRepository;

        private readonly IMapper _mapper;

        public GetCorrectiveByParametersSpRequestHandler(ISchoolManagementRepository<Demand> studentInfoByTraineeIdRepository, IMapper mapper)
        {
            _studentInfoByTraineeIdRepository = studentInfoByTraineeIdRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetCorrectiveByParametersSpRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [spFindCorrectiveByParameters] {0},{1},{2},{3},{4}", request.UnitId, request.DepartmentId, request.ItemCategoryId, request.EquipmentNameId, request.ItemNameId);

            DataTable dataTable = _studentInfoByTraineeIdRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
