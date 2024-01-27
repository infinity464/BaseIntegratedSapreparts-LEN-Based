using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.ItemStors.Requests.Queries;
using System.Data;
using SchoolManagement.Application.Features.AirCraftNames.Requests.Queries;

namespace SchoolManagement.Application.Features.ItemStors.Handlers.Queries
{ 
    public class GetSpareStockByParameterSpRequestHandler : IRequestHandler<GetSpareStockByParameterSpRequest, object>
    {

        private readonly ISchoolManagementRepository<ItemStor> _FlyingTimeByAricraftRepository;

        private readonly IMapper _mapper;

        public GetSpareStockByParameterSpRequestHandler(ISchoolManagementRepository<ItemStor> FlyingTimeByAricraftRepository, IMapper mapper)
        {
            _FlyingTimeByAricraftRepository = FlyingTimeByAricraftRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSpareStockByParameterSpRequest request, CancellationToken cancellationToken)
        {
           // object obj = new object();
            var spQuery = String.Format("exec [spGetSpareStockForCorrectiveMaintenence] {0},'{1}',{2}", request.UnitId,request.DepartmentNameId,request.EquipmentNameId);

            DataTable dataTable = _FlyingTimeByAricraftRepository.ExecWithSqlQuery(spQuery);
           
            return dataTable;
         
        }
    }
}
