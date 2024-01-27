using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.IssueType;
using SchoolManagement.Application.Features.IssueTypes.Requests.Queries;

namespace SchoolManagement.Application.Features.IssueTypes.Handlers.Queries
{
    public class GetIssueTypeListRequestHandler : IRequestHandler<GetIssueTypeListRequest, PagedResult<IssueTypeDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.IssueType> _IssueTypeRepository;

        private readonly IMapper _mapper;

        public GetIssueTypeListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.IssueType> IssueTypeRepository, IMapper mapper)
        {
            _IssueTypeRepository = IssueTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<IssueTypeDto>> Handle(GetIssueTypeListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.IssueType> IssueTypes = _IssueTypeRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = IssueTypes.Count();
            IssueTypes = IssueTypes.OrderByDescending(x => x.IssueTypeId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var IssueTypeDtos = _mapper.Map<List<IssueTypeDto>>(IssueTypes);
            var result = new PagedResult<IssueTypeDto>(IssueTypeDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
