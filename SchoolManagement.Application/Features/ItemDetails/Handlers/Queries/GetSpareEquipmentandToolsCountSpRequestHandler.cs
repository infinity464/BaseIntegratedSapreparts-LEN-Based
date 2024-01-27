using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.EquipmentNames.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.EquipmentNames.Handlers.Queries
{
    public class GetSpareEquipmentandToolsCountSpRequestHandler : IRequestHandler<GetSpareEquipmentandToolsCountSpRequest, object>
    {

        private readonly ISchoolManagementRepository<ItemDetail> _ItemDetailRepository;

        private readonly IMapper _mapper;
         
        public GetSpareEquipmentandToolsCountSpRequestHandler(ISchoolManagementRepository<ItemDetail> ItemDetailRepository, IMapper mapper)
        {
            _ItemDetailRepository = ItemDetailRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSpareEquipmentandToolsCountSpRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [spGetSpareEquipmentandToolsCount] {0},{1},{2}", request.UnitId,request.DepartmentId, request.ItemCategoryId);

            DataTable dataTable = _ItemDetailRepository.ExecWithSqlQuery(spQuery);

           
            return dataTable;
         
        }
    }
}
