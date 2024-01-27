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
    public class GetIssueRegisterListForSurveyRequestHandler :  IRequestHandler<GetIssueRegisterListForSurveyRequest, List<IssueRegisterDto>>
    {
        private readonly ISchoolManagementRepository<IssueRegister> _issueRegisterRepository;
        private readonly IMapper _mapper;


        public GetIssueRegisterListForSurveyRequestHandler(ISchoolManagementRepository<IssueRegister> issueRegisterRepository, IMapper mapper)
        {
            _issueRegisterRepository = issueRegisterRepository;
            _mapper = mapper;
        }

        public async Task<List<IssueRegisterDto>> Handle(GetIssueRegisterListForSurveyRequest request, CancellationToken cancellationToken)
        {
            IQueryable<IssueRegister> IssueRegisterList = _issueRegisterRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == 1 && x.SurveyStatus == 1 && x.SurveyNumber != null, "ItemDetail", "ToolsLocation", "SurveyAuthority", "SurveyItemType");
            
            var IssueRegisterDtos = _mapper.Map<List<IssueRegisterDto>>(IssueRegisterList);

            return IssueRegisterDtos;
        }
    }
}
