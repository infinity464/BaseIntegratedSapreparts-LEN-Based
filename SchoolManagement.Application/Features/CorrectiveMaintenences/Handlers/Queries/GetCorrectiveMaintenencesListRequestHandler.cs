using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Queries;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Handlers.Queries
{
    public class GetCorrectiveMaintenenceListRequestHandler : IRequestHandler<GetCorrectiveMaintenenceListRequest, PagedResult<CorrectiveMaintenenceDto>>
    {

        private readonly ISchoolManagementRepository<CorrectiveMaintenence> _CorrectiveMaintenenceRepository;

        private readonly IMapper _mapper;

        public GetCorrectiveMaintenenceListRequestHandler(ISchoolManagementRepository<CorrectiveMaintenence> CorrectiveMaintenenceRepository, IMapper mapper)
        {
            _CorrectiveMaintenenceRepository = CorrectiveMaintenenceRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<CorrectiveMaintenenceDto>> Handle(GetCorrectiveMaintenenceListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<CorrectiveMaintenence> CorrectiveMaintenences = _CorrectiveMaintenenceRepository.FilterWithInclude(x => String.IsNullOrEmpty(request.QueryParams.SearchText), "StatusOfJob", "ItemDetail");
            var totalCount = CorrectiveMaintenences.Count();
            CorrectiveMaintenences = CorrectiveMaintenences.OrderByDescending(x => x.CorrectiveMaintenenceId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize).Where(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.IssueRegisterId == request.IssueRegisterId);

            var CorrectiveMaintenenceDtos = _mapper.Map<List<CorrectiveMaintenenceDto>>(CorrectiveMaintenences);
            var result = new PagedResult<CorrectiveMaintenenceDto>(CorrectiveMaintenenceDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;
        }
    }
}
