using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReturnLogs.Validators;
using SchoolManagement.Application.Features.ReturnLogs.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReturnLogs.Handlers.Commands
{
    public class CreateReturnLogCommandHandler : IRequestHandler<CreateReturnLogCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReturnLogCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateReturnLogCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            //var validator = new CreateReturnLogDtoValidator();
            //var validationResult = await validator.ValidateAsync(request.ReturnLogDto);

            //if (validationResult.IsValid == false)
            //{
            //    response.Success = false;
            //    response.Message = "Creation Failed";
            //    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            //}
            //else
            //{
                var ReturnLog = _mapper.Map<ReturnLog>(request.ReturnLogDto);

                ReturnLog = await _unitOfWork.Repository<ReturnLog>().Add(ReturnLog);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                //response.Success = true;
                //response.Message = "Creation Successful";
                //response.Id = ReturnLog.ReturnLogId;
      //      }

            return response;
        }
    }
}
