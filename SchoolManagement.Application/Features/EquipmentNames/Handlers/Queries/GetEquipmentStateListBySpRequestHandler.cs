using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.EquipmentNames.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.EquipmentNames.Handlers.Queries
{
    public class GetEquipmentStateListBySpRequestHandler : IRequestHandler<GetEquipmentStateListBySpRequest, object>
    {

        private readonly ISchoolManagementRepository<Demand> _DemandRepository;

        private readonly IMapper _mapper;

        public GetEquipmentStateListBySpRequestHandler(ISchoolManagementRepository<Demand> DemandRepository, IMapper mapper)
        {
            _DemandRepository = DemandRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetEquipmentStateListBySpRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [spGetEquipmentStateCountForDb] {0},{1},{2}", request.UnitId,request.DepartmentId, request.OperationStatus);

            DataTable dataTable = _DemandRepository.ExecWithSqlQuery(spQuery);

           
            return dataTable;
         
        }
    }
}
