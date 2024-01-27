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
    public class GetIssueRegisterListFromStoreRequestHandler :  IRequestHandler<GetIssueRegisterListFromStoreRequest, List<ItemStorDto>>
    {
        private readonly ISchoolManagementRepository<ItemStor> _ItemStorRepository;
        private readonly IMapper _mapper;


        public GetIssueRegisterListFromStoreRequestHandler(ISchoolManagementRepository<ItemStor> ItemStorRepository, IMapper mapper)
        {
            _ItemStorRepository = ItemStorRepository;
            _mapper = mapper;
        }

        public async Task<List<ItemStorDto>> Handle(GetIssueRegisterListFromStoreRequest request, CancellationToken cancellationToken)
        {
            IQueryable<ItemStor> ItemStors = _ItemStorRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId  && x.ItemDetailId == request.ItemDetailId, "ItemDetail", "ToolsLocation").Where(x=>x.AvailableQty !=0);
            
            var IssueRegisterDtos = _mapper.Map<List<ItemStorDto>>(ItemStors);

            return IssueRegisterDtos;
        }
    }
}
