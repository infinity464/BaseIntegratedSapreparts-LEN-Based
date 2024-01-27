using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Progres;
using SchoolManagement.Application.Features.Progress.Requests.Queries;

namespace SchoolManagement.Application.Features.Progress.Handlers.Queries
{
    public class GetProgresListRequestHandler : IRequestHandler<GetProgresListRequest, PagedResult<ProgresDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Progres> _ProgresRepository;

        private readonly IMapper _mapper;

        public GetProgresListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Progres> ProgresRepository, IMapper mapper)
        {
            _ProgresRepository = ProgresRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProgresDto>> Handle(GetProgresListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.Progres> Progress = _ProgresRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Progress.Count();
            Progress = Progress.OrderByDescending(x => x.ProgresId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ProgresDtos = _mapper.Map<List<ProgresDto>>(Progress);
            var result = new PagedResult<ProgresDto>(ProgresDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}
