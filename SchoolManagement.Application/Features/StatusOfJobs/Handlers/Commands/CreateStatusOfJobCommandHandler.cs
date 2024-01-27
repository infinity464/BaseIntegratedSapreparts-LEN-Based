using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.StatusOfJob.Validators;
using SchoolManagement.Application.Features.StatusOfJobs.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.StatusOfJobs.Handlers.Commands
{
    public class CreateStatusOfJobCommandHandler : IRequestHandler<CreateStatusOfJobCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateStatusOfJobCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateStatusOfJobCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateStatusOfJobDtoValidator();
            var validationResult = await validator.ValidateAsync(request.StatusOfJobDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var StatusOfJob = _mapper.Map<StatusOfJob>(request.StatusOfJobDto);

                StatusOfJob = await _unitOfWork.Repository<StatusOfJob>().Add(StatusOfJob);

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
                response.Id = StatusOfJob.StatusOfJobId;
            }

            return response;
        }
    }
}
