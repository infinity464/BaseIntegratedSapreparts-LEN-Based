using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.PmsNumberEntry.Validators;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Handlers.Commands
{
    public class CreatePmsNumberEntryCommandHandler : IRequestHandler<CreatePmsNumberEntryCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePmsNumberEntryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreatePmsNumberEntryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreatePmsNumberEntryDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PmsNumberEntryDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var PmsNumberEntry = _mapper.Map<PmsNumberEntry>(request.PmsNumberEntryDto);

                PmsNumberEntry = await _unitOfWork.Repository<PmsNumberEntry>().Add(PmsNumberEntry);

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
                response.Id = PmsNumberEntry.PmsNumberEntryId;
            }

            return response;
        }
    }
}
