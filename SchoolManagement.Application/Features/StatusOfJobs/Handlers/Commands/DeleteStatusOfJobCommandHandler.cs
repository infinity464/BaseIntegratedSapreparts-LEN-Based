using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.StatusOfJobs.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.StatusOfJobs.Handlers.Commands
{
    public class DeleteStatusOfJobCommandHandler : IRequestHandler<DeleteStatusOfJobCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteStatusOfJobCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteStatusOfJobCommand request, CancellationToken cancellationToken)
        {
            var StatusOfJob = await _unitOfWork.Repository<StatusOfJob>().Get(request.StatusOfJobId);

            if (StatusOfJob == null)
                throw new NotFoundException(nameof(StatusOfJob), request.StatusOfJobId);

            await _unitOfWork.Repository<StatusOfJob>().Delete(StatusOfJob);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
