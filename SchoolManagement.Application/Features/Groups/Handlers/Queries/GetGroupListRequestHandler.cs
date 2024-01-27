using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Group;
using SchoolManagement.Application.Features.Groups.Requests.Queries;

namespace SchoolManagement.Application.Features.Groups.Handlers.Queries
{
    public class GetGroupListRequestHandler : IRequestHandler<GetGroupListRequest, PagedResult<GroupDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Group> _GroupRepository;

        private readonly IMapper _mapper;

        public GetGroupListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Group> GroupRepository, IMapper mapper)
        {
            _GroupRepository = GroupRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<GroupDto>> Handle(GetGroupListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.Group> Groups = _GroupRepository.FilterWithInclude(x => (x.GroupName.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Groups.Count();
            Groups = Groups.OrderByDescending(x => x.GroupId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var GroupDtos = _mapper.Map<List<GroupDto>>(Groups);
            var result = new PagedResult<GroupDto>(GroupDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
