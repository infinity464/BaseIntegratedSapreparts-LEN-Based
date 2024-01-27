using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SpareRequiredEntry.Validators;
using SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Handlers.Commands
{
    public class CreateSpareRequiredEntryCommandHandler : IRequestHandler<CreateSpareRequiredEntryCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSpareRequiredEntryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateSpareRequiredEntryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSpareRequiredEntryDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SpareRequiredEntryDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var SpareRequiredEntry = _mapper.Map<SpareRequiredEntry>(request.SpareRequiredEntryDto);

                SpareRequiredEntry = await _unitOfWork.Repository<SpareRequiredEntry>().Add(SpareRequiredEntry);

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
                response.Id = SpareRequiredEntry.SpareRequiredEntryId;
            }

            return response;
        }
    }
}
