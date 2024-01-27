using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Acceptances.Validators;
using SchoolManagement.Application.Features.Acceptances.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Acceptances.Handlers.Commands
{
    public class CreateAcceptanceCommandHandler : IRequestHandler<CreateAcceptanceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAcceptanceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateAcceptanceCommand request, CancellationToken cancellationToken)
        {
             var response = new BaseCommandResponse();
            var validator = new CreateAcceptanceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.AcceptanceDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {

                /////// File Upload //////////
                ///
                string uniqueFileName = null; 

                if (request.AcceptanceDto.Doc != null)
                {
                    var fileName = Path.GetFileName(request.AcceptanceDto.Doc.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\acceptances", uniqueFileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.AcceptanceDto.Doc.CopyToAsync(fileSteam);
                    }
                }

                var acceptances = _mapper.Map<Acceptance>(request.AcceptanceDto);

                acceptances.AcceptanceDocument = request.AcceptanceDto.AcceptanceDocument ?? "files/acceptances/" + uniqueFileName;

                acceptances = await _unitOfWork.Repository<Acceptance>().Add(acceptances);
                acceptances.AcceptanceDate = acceptances.AcceptanceDate.Value.AddDays(1.0);
                await _unitOfWork.Save();

                var demands = await _unitOfWork.Repository<Demand>().Get((int)request.AcceptanceDto.DemandId);

                demands.SftStatus = 1;
                demands.ItemStatusId = 8;

                await _unitOfWork.Repository<Demand>().Update(demands);

                await _unitOfWork.Save();

               


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = acceptances.DemandId.Value;
            }

            return response;
        }
    }
}
