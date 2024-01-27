using SchoolManagement.Application.Features.Acceptances.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.Acceptances;

namespace SchoolManagement.Application.Features.Acceptances.Handlers.Queries
{
    public class GetSftListRequestHandler : IRequestHandler<GetSftListRequest, PagedResult<AcceptanceDto>>
    {

        private readonly ISchoolManagementRepository<Acceptance> _AcceptanceRepository;

        private readonly IMapper _mapper;

        public GetSftListRequestHandler(ISchoolManagementRepository<Acceptance> AcceptanceRepository, IMapper mapper)
        {
            _AcceptanceRepository = AcceptanceRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<AcceptanceDto>> Handle(GetSftListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

                var Acceptances = _AcceptanceRepository.FilterWithInclude(x =>String.IsNullOrEmpty(request.QueryParams.SearchText)).ToList();
                var totalCount = Acceptances.Count();
                Acceptances = Acceptances.OrderByDescending(x => x.AcceptanceId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize).ToList();

                var AcceptanceDtos = _mapper.Map<List<AcceptanceDto>>(Acceptances);
                var result = new PagedResult<AcceptanceDto>(AcceptanceDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

                return result;
            

        }
    }
}
