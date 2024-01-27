using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.StatusOfJob.Validators;
using SchoolManagement.Application.Features.StatusOfJobs.Requests.Commands;

namespace SchoolManagement.Application.Features.StatusOfJobs.Handlers.Commands
{
    public class UpdateStatusOfJobCommandHandler : IRequestHandler<UpdateStatusOfJobCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateStatusOfJobCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateStatusOfJobCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateStatusOfJobDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.StatusOfJobDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var StatusOfJob = await _unitOfWork.Repository<StatusOfJob>().Get(request.StatusOfJobDto.StatusOfJobId);

            if (StatusOfJob is null)
                throw new NotFoundException(nameof(StatusOfJob), request.StatusOfJobDto.StatusOfJobId);

            _mapper.Map(request.StatusOfJobDto, StatusOfJob);

            await _unitOfWork.Repository<StatusOfJob>().Update(StatusOfJob);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
