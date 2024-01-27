using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetDemandProcurementAndSftBySpRequestHandler : IRequestHandler<GetDemandProcurementAndSftBySpRequest, object>
    {

        private readonly ISchoolManagementRepository<Demand> _studentInfoByTraineeIdRepository;

        private readonly IMapper _mapper;

        public GetDemandProcurementAndSftBySpRequestHandler(ISchoolManagementRepository<Demand> studentInfoByTraineeIdRepository, IMapper mapper)
        {
            _studentInfoByTraineeIdRepository = studentInfoByTraineeIdRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetDemandProcurementAndSftBySpRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [spGetDemandProcurementAndSftInformation] {0}", request.DemandId);

            DataTable dataTable = _studentInfoByTraineeIdRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
