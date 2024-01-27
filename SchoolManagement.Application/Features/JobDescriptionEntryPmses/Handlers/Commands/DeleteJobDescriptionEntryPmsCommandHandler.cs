using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Handlers.Commands
{
    public class DeleteJobDescriptionEntryPmsCommandHandler : IRequestHandler<DeleteJobDescriptionEntryPmsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteJobDescriptionEntryPmsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteJobDescriptionEntryPmsCommand request, CancellationToken cancellationToken)
        {
            var JobDescriptionEntryPms = await _unitOfWork.Repository<JobDescriptionEntryPms>().Get(request.JobDescriptionEntryPmsId);

            if (JobDescriptionEntryPms == null)
                throw new NotFoundException(nameof(JobDescriptionEntryPms), request.JobDescriptionEntryPmsId);

            await _unitOfWork.Repository<JobDescriptionEntryPms>().Delete(JobDescriptionEntryPms);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
