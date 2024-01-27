using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.JobDescriptions.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.JobDescriptions.Handlers.Commands
{
    public class DeleteJobDescriptionCommandHandler : IRequestHandler<DeleteJobDescriptionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteJobDescriptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteJobDescriptionCommand request, CancellationToken cancellationToken)
        {
            var JobDescription = await _unitOfWork.Repository<JobDescription>().Get(request.JobDescriptionId);

            if (JobDescription == null)
                throw new NotFoundException(nameof(JobDescription), request.JobDescriptionId);

            await _unitOfWork.Repository<JobDescription>().Delete(JobDescription);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
