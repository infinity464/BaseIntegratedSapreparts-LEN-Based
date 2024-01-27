using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences.Validators;
using SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Commands;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Handlers.Commands
{
    public class UpdateJobDescriptionScheduleMaintenenceCommandHandler : IRequestHandler<UpdateJobDescriptionScheduleMaintenenceCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateJobDescriptionScheduleMaintenenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateJobDescriptionScheduleMaintenenceCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateJobDescriptionScheduleMaintenenceDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.JobDescriptionScheduleMaintenenceDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var JobDescriptionScheduleMaintenence = await _unitOfWork.Repository<JobDescriptionScheduleMaintenence>().Get(request.JobDescriptionScheduleMaintenenceDto.JobDescriptionScheduleMaintenenceId);

            if (JobDescriptionScheduleMaintenence is null)
                throw new NotFoundException(nameof(JobDescriptionScheduleMaintenence), request.JobDescriptionScheduleMaintenenceDto.JobDescriptionScheduleMaintenenceId);

            _mapper.Map(request.JobDescriptionScheduleMaintenenceDto, JobDescriptionScheduleMaintenence);

            await _unitOfWork.Repository<JobDescriptionScheduleMaintenence>().Update(JobDescriptionScheduleMaintenence);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
