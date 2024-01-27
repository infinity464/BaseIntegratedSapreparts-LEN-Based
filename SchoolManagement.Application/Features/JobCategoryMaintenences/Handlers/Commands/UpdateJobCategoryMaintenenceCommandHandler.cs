using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobCategoryMaintenences.Validators;
using SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Commands;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Handlers.Commands
{
    public class UpdateJobCategoryMaintenenceCommandHandler : IRequestHandler<UpdateJobCategoryMaintenenceCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateJobCategoryMaintenenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateJobCategoryMaintenenceCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateJobCategoryMaintenenceDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.JobCategoryMaintenenceDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var JobCategoryMaintenence = await _unitOfWork.Repository<JobCategoryMaintenence>().Get(request.JobCategoryMaintenenceDto.JobCategoryMaintenenceId);

            if (JobCategoryMaintenence is null)
                throw new NotFoundException(nameof(JobCategoryMaintenence), request.JobCategoryMaintenenceDto.JobCategoryMaintenenceId);

            _mapper.Map(request.JobCategoryMaintenenceDto, JobCategoryMaintenence);

            await _unitOfWork.Repository<JobCategoryMaintenence>().Update(JobCategoryMaintenence);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
