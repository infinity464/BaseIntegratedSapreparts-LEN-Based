using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobCategory.Validators;
using SchoolManagement.Application.Features.JobCategorys.Requests.Commands;

namespace SchoolManagement.Application.Features.JobCategorys.Handlers.Commands
{
    public class UpdateJobCategoryCommandHandler : IRequestHandler<UpdateJobCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateJobCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateJobCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateJobCategoryDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.JobCategoryDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var JobCategory = await _unitOfWork.Repository<JobCategory>().Get(request.JobCategoryDto.JobCategoryId);

            if (JobCategory is null)
                throw new NotFoundException(nameof(JobCategory), request.JobCategoryDto.JobCategoryId);

            _mapper.Map(request.JobCategoryDto, JobCategory);

            await _unitOfWork.Repository<JobCategory>().Update(JobCategory);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
