using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class FindDemandStatesSpRequestHandler : IRequestHandler<FindDemandStatesSpRequest, object>
    {

        private readonly ISchoolManagementRepository<Demand> _DemandRepository;

        private readonly IMapper _mapper;

        public FindDemandStatesSpRequestHandler(ISchoolManagementRepository<Demand> DemandRepository, IMapper mapper)
        {
            _DemandRepository = DemandRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(FindDemandStatesSpRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [spFindDemandStates] {0},{1}", request.UnitId, request.DepartmentId);

            DataTable dataTable = _DemandRepository.ExecWithSqlQuery(spQuery);

           
            return dataTable;
         
        }
    }
}
