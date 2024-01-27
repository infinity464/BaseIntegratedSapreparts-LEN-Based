using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.IssueRegisters.Handlers.Queries
{
    public class GetIssueRegisterListByParameterRequestHandler :  IRequestHandler<GetIssueRegisterListByParameterRequest, List<IssueRegisterDto>>
    {
        private readonly ISchoolManagementRepository<IssueRegister> _IssueRegisterRepository;
        private readonly IMapper _mapper;


        public GetIssueRegisterListByParameterRequestHandler(ISchoolManagementRepository<IssueRegister> IssueRegisterRepository, IMapper mapper)
        {
            _IssueRegisterRepository = IssueRegisterRepository;
            _mapper = mapper;
        }

        

        public async Task<List<IssueRegisterDto>> Handle(GetIssueRegisterListByParameterRequest request, CancellationToken cancellationToken)
        {
            IQueryable<IssueRegister> IssueRegisters = _IssueRegisterRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId  && x.IssueTypeId == request.IssueTypeId, "ItemDetail", "EquipmentName");
            

            var IssueRegisterDtos = _mapper.Map<List<IssueRegisterDto>>(IssueRegisters);

            return IssueRegisterDtos;
        }
    }
}
