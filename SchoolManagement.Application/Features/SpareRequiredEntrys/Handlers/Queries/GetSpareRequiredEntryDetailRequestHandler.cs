using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SpareRequiredEntry;
using SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Handlers.Queries
{
    public class GetSpareRequiredEntryDetailRequestHandler : IRequestHandler<GetSpareRequiredEntryDetailRequest, SpareRequiredEntryDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SpareRequiredEntry> _SpareRequiredEntryRepository;
        public GetSpareRequiredEntryDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SpareRequiredEntry> SpareRequiredEntryRepository, IMapper mapper)
        {
            _SpareRequiredEntryRepository = SpareRequiredEntryRepository;
            _mapper = mapper;
        }
        public async Task<SpareRequiredEntryDto> Handle(GetSpareRequiredEntryDetailRequest request, CancellationToken cancellationToken)
        {
            var SpareRequiredEntry = await _SpareRequiredEntryRepository.Get(request.SpareRequiredEntryId);
            return _mapper.Map<SpareRequiredEntryDto>(SpareRequiredEntry);
        }
    }
}
