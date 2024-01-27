using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms.Validators;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Commands;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Handlers.Commands
{
    public class UpdateJobDescriptionEntryPmsCommandHandler : IRequestHandler<UpdateJobDescriptionEntryPmsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateJobDescriptionEntryPmsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateJobDescriptionEntryPmsCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateJobDescriptionEntryPmsDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.JobDescriptionEntryPmsDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var JobDescriptionEntryPms = await _unitOfWork.Repository<JobDescriptionEntryPms>().Get(request.JobDescriptionEntryPmsDto.JobDescriptionEntryPmsId);

            if (JobDescriptionEntryPms is null)
                throw new NotFoundException(nameof(JobDescriptionEntryPms), request.JobDescriptionEntryPmsDto.JobDescriptionEntryPmsId);

            _mapper.Map(request.JobDescriptionEntryPmsDto, JobDescriptionEntryPms);

            await _unitOfWork.Repository<JobDescriptionEntryPms>().Update(JobDescriptionEntryPms);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
