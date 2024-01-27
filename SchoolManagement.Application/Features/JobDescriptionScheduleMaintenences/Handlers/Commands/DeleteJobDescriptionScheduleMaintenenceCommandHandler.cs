using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Handlers.Commands
{
    public class DeleteJobDescriptionScheduleMaintenenceCommandHandler : IRequestHandler<DeleteJobDescriptionScheduleMaintenenceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteJobDescriptionScheduleMaintenenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteJobDescriptionScheduleMaintenenceCommand request, CancellationToken cancellationToken)
        {
            var JobDescriptionScheduleMaintenence = await _unitOfWork.Repository<JobDescriptionScheduleMaintenence>().Get(request.JobDescriptionScheduleMaintenenceId);

            if (JobDescriptionScheduleMaintenence == null)
                throw new NotFoundException(nameof(JobDescriptionScheduleMaintenence), request.JobDescriptionScheduleMaintenenceId);

            await _unitOfWork.Repository<JobDescriptionScheduleMaintenence>().Delete(JobDescriptionScheduleMaintenence);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
