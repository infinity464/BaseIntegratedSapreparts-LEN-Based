using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Handlers.Commands
{
    public class DeleteJobCategoryMaintenenceCommandHandler : IRequestHandler<DeleteJobCategoryMaintenenceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteJobCategoryMaintenenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteJobCategoryMaintenenceCommand request, CancellationToken cancellationToken)
        {
            var JobCategoryMaintenence = await _unitOfWork.Repository<JobCategoryMaintenence>().Get(request.JobCategoryMaintenenceId);

            if (JobCategoryMaintenence == null)
                throw new NotFoundException(nameof(JobCategoryMaintenence), request.JobCategoryMaintenenceId);

            await _unitOfWork.Repository<JobCategoryMaintenence>().Delete(JobCategoryMaintenence);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
