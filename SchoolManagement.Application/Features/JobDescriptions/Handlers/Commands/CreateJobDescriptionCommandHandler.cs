using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobDescription.Validators;
using SchoolManagement.Application.Features.JobDescriptions.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.JobDescriptions.Handlers.Commands
{
    public class CreateJobDescriptionCommandHandler : IRequestHandler<CreateJobDescriptionCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateJobDescriptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateJobDescriptionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateJobDescriptionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.JobDescriptionDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var JobDescription = _mapper.Map<JobDescription>(request.JobDescriptionDto);

                JobDescription = await _unitOfWork.Repository<JobDescription>().Add(JobDescription);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = JobDescription.JobDescriptionId;
            }

            return response;
        }
    }
}
