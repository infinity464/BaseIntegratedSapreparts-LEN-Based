using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Data;

namespace SchoolManagement.Application.Features.IssueRegisters.Handlers.Queries
{
    public class GetIssueRegisterListForSurveyDeclareByParameterRequestHandler : IRequestHandler<GetIssueRegisterListForSurveyDeclareByParameterRequest, object>
    {

        private readonly ISchoolManagementRepository<IssueRegister> _studentInfoByTraineeIdRepository;

        private readonly IMapper _mapper;

        public GetIssueRegisterListForSurveyDeclareByParameterRequestHandler(ISchoolManagementRepository<IssueRegister> studentInfoByTraineeIdRepository, IMapper mapper)
        {
            _studentInfoByTraineeIdRepository = studentInfoByTraineeIdRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetIssueRegisterListForSurveyDeclareByParameterRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [spGetSurveyDeclaer] {0},{1},{2},{3}", request.UnitId, request.DepartmentNameId,request.ItemCategoryId,request.ItemDetailId);

            DataTable dataTable = _studentInfoByTraineeIdRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
