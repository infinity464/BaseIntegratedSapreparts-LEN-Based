using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Commands;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence.Validators;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Handlers.Commands
{
    public class UpdateCorrectiveMaintenenceCommandHandler : IRequestHandler<UpdateCorrectiveMaintenenceCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCorrectiveMaintenenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCorrectiveMaintenenceCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCorrectiveMaintenenceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CorrectiveMaintenenceDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var CorrectiveMaintenence = await _unitOfWork.Repository<CorrectiveMaintenence>().Get(request.CorrectiveMaintenenceDto.CorrectiveMaintenenceId);

            if (CorrectiveMaintenence is null)
                throw new NotFoundException(nameof(CorrectiveMaintenence), request.CorrectiveMaintenenceDto.CorrectiveMaintenenceId);

            /////// Image Upload //////////
            ///
            string uniqueFileName = null;
            //CreateCorrectiveMaintenenceDto
            if (request.CorrectiveMaintenenceDto.Doc != null)
            {

                var fileName = Path.GetFileName(request.CorrectiveMaintenenceDto.Doc.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Content\\images\\profile", uniqueFileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\corrective-maintenence", uniqueFileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.CorrectiveMaintenenceDto.Doc.CopyToAsync(fileSteam);
                }


            }
            _mapper.Map(request.CorrectiveMaintenenceDto, CorrectiveMaintenence);
            CorrectiveMaintenence.RefDocument = request.CorrectiveMaintenenceDto.Doc != null ? "files/corrective-maintenence/" + uniqueFileName : CorrectiveMaintenence.RefDocument.Replace("https://localhost:44395/Content/", String.Empty);
            await _unitOfWork.Repository<CorrectiveMaintenence>().Update(CorrectiveMaintenence);
            await _unitOfWork.Save();

            return Unit.Value;

            //var validator = new UpdateCorrectiveMaintenenceDtoValidator(); 
            // var validationResult = await validator.ValidateAsync(request.CorrectiveMaintenenceDto);

            //if (validationResult.IsValid == false)
            //    throw new ValidationException(validationResult);

            //var CorrectiveMaintenence = await _unitOfWork.Repository<CorrectiveMaintenence>().Get(request.CorrectiveMaintenenceDto.CorrectiveMaintenenceId);

            //if (CorrectiveMaintenence is null)
            //    throw new NotFoundException(nameof(CorrectiveMaintenence), request.CorrectiveMaintenenceDto.CorrectiveMaintenenceId);

            //_mapper.Map(request.CorrectiveMaintenenceDto, CorrectiveMaintenence);

            //await _unitOfWork.Repository<CorrectiveMaintenence>().Update(CorrectiveMaintenence);
            //await _unitOfWork.Save();

            //return Unit.Value;
        }
    }
}
