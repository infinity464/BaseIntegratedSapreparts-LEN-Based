using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.ItemDetails.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.ItemDetails.Handlers.Queries
{
    public class GetItemDetailByParametersSpRequestHandler : IRequestHandler<GetItemDetailByParametersSpRequest, object>
    {

        private readonly ISchoolManagementRepository<Demand> _studentInfoByTraineeIdRepository;

        private readonly IMapper _mapper;

        public GetItemDetailByParametersSpRequestHandler(ISchoolManagementRepository<Demand> studentInfoByTraineeIdRepository, IMapper mapper)
        {
            _studentInfoByTraineeIdRepository = studentInfoByTraineeIdRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetItemDetailByParametersSpRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [spGetItemDetailByParameters] {0},{1},{2},{3}", request.UnitId, request.DepartmentId, request.ItemCategoryId, request.EquipmentNameId);

            DataTable dataTable = _studentInfoByTraineeIdRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
