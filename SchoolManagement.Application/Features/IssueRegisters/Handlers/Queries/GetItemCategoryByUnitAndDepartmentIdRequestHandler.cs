using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.IssueRegisters.Handlers.Queries
{
    public class GetItemCategoryByUnitAndDepartmentIdRequestHandler : IRequestHandler<GetItemCategoryByUnitAndDepartmentIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<IssueRegister> _IssueRegisterRepository;


        public GetItemCategoryByUnitAndDepartmentIdRequestHandler(ISchoolManagementRepository<IssueRegister> IssueRegisterRepository)
        {
            _IssueRegisterRepository = IssueRegisterRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetItemCategoryByUnitAndDepartmentIdRequest request, CancellationToken cancellationToken)
        {
            var codeValues = _IssueRegisterRepository.FilterWithInclude(x => x.IsActive && x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId, "ItemCategory");
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.ItemCategory.Name,
                Value = x.IssueRegisterId
            }).ToList();
            return selectModels;
        }
    }
}
