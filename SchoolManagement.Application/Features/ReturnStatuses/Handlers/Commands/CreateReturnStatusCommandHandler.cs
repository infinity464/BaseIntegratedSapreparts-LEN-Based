using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReturnStatus.Validators;
using SchoolManagement.Application.Features.ReturnStatuss.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReturnStatuss.Handlers.Commands
{
    public class CreateReturnStatusCommandHandler : IRequestHandler<CreateReturnStatusCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReturnStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateReturnStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateReturnStatusDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ReturnStatusDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ReturnStatus = _mapper.Map<ReturnStatus>(request.ReturnStatusDto);

                ReturnStatus = await _unitOfWork.Repository<ReturnStatus>().Add(ReturnStatus);

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
                response.Id = ReturnStatus.ReturnStatusId;
            }

            return response;
        }
    }
}
