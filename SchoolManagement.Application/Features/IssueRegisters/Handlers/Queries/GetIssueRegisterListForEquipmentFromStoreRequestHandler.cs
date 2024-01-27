using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.DTOs.ItemStor;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.IssueRegisters.Handlers.Queries
{
    public class GetIssueRegisterListForEquipmentFromStoreRequestHandler :  IRequestHandler<GetIssueRegisterListForEquipmentFromStoreRequest, List<ItemStorDto>>
    {
        private readonly ISchoolManagementRepository<ItemStor> _ItemStorRepository;
        private readonly IMapper _mapper;

         
        public GetIssueRegisterListForEquipmentFromStoreRequestHandler(ISchoolManagementRepository<ItemStor> ItemStorRepository, IMapper mapper)
        {
            _ItemStorRepository = ItemStorRepository;
            _mapper = mapper;
        }

        public async Task<List<ItemStorDto>> Handle(GetIssueRegisterListForEquipmentFromStoreRequest request, CancellationToken cancellationToken)
        {
            IQueryable<ItemStor> ItemStors = _ItemStorRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId  && x.ItemDetailId == request.ItemDetailId, "ItemDetail","ToolsLocation").Where(x=>x.AvailableQty !=0);
            
            var IssueRegisterDtos = _mapper.Map<List<ItemStorDto>>(ItemStors);

            foreach (var item in IssueRegisterDtos)
            {
                item.IssuedQty = 1;
            }

            return IssueRegisterDtos;
        }
    }
}
