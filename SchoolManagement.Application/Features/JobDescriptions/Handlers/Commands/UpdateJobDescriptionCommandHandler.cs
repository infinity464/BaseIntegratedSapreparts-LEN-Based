using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobDescription.Validators;
using SchoolManagement.Application.Features.JobDescriptions.Requests.Commands;

namespace SchoolManagement.Application.Features.JobDescriptions.Handlers.Commands
{
    public class UpdateJobDescriptionCommandHandler : IRequestHandler<UpdateJobDescriptionCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateJobDescriptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateJobDescriptionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateJobDescriptionDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.JobDescriptionDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var JobDescription = await _unitOfWork.Repository<JobDescription>().Get(request.JobDescriptionDto.JobDescriptionId);

            if (JobDescription is null)
                throw new NotFoundException(nameof(JobDescription), request.JobDescriptionDto.JobDescriptionId);

            _mapper.Map(request.JobDescriptionDto, JobDescription);

            await _unitOfWork.Repository<JobDescription>().Update(JobDescription);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
