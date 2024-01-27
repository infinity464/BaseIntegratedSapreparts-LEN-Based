using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.SpareRequiredEntry;
using SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Queries;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Handlers.Queries
{
    public class GetSpareRequiredEntryByDepartmentRequestHandler : IRequestHandler<GetSpareRequiredEntryByDepartmentRequest, List<SpareRequiredEntryDto>>
    {
        private readonly ISchoolManagementRepository<SpareRequiredEntry> _SpareRequiredEntryRepository;

        private readonly IMapper _mapper;
        public GetSpareRequiredEntryByDepartmentRequestHandler(ISchoolManagementRepository<SpareRequiredEntry> SpareRequiredEntryRepository, IMapper mapper)
        {
            _SpareRequiredEntryRepository = SpareRequiredEntryRepository;
            _mapper = mapper;
        }

        public async Task<List<SpareRequiredEntryDto>> Handle(GetSpareRequiredEntryByDepartmentRequest request, CancellationToken cancellationToken)
        {
            IQueryable<SpareRequiredEntry> SpareRequiredEntrys = _SpareRequiredEntryRepository.FilterWithInclude(x => x.DepartmentNameId == request.DepartmentNameId, "DepartmentName", "ItemDetail", "JobDescriptionEntryPms");

            var SpareRequiredEntryDtos = _mapper.Map<List<SpareRequiredEntryDto>>(SpareRequiredEntrys);

            return SpareRequiredEntryDtos;
        }

    }
}
