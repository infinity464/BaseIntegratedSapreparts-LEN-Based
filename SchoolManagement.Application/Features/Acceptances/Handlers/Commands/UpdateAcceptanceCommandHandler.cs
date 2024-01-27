using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Acceptances.Validators;
using SchoolManagement.Application.Features.Acceptances.Requests.Commands;

namespace SchoolManagement.Application.Features.Acceptances.Handlers.Commands
{
    public class UpdateAcceptanceCommandHandler : IRequestHandler<UpdateAcceptanceCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAcceptanceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateAcceptanceCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateAcceptanceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.AcceptanceDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Acceptance = await _unitOfWork.Repository<Acceptance>().Get(request.AcceptanceDto.AcceptanceId);

            if (Acceptance is null)
                throw new NotFoundException(nameof(Acceptance), request.AcceptanceDto.AcceptanceId);

            /////// Image Upload //////////
            ///
            string uniqueFileName = null;
            //CreateAcceptanceDto
            if (request.AcceptanceDto.Doc != null)
            {

                var fileName = Path.GetFileName(request.AcceptanceDto.Doc.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Content\\images\\profile", uniqueFileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\acceptances", uniqueFileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.AcceptanceDto.Doc.CopyToAsync(fileSteam);
                }


            }
            _mapper.Map(request.AcceptanceDto, Acceptance);
            Acceptance.AcceptanceDocument = request.AcceptanceDto.Doc != null ? "files/acceptances/" + uniqueFileName : Acceptance.AcceptanceDocument.Replace("https://localhost:44395/Content/", String.Empty);
            await _unitOfWork.Repository<Acceptance>().Update(Acceptance);
            await _unitOfWork.Save();

            return Unit.Value;

            //var validator = new UpdateAcceptanceDtoValidator(); 
            // var validationResult = await validator.ValidateAsync(request.AcceptanceDto);

            //if (validationResult.IsValid == false)
            //    throw new ValidationException(validationResult);

            //var Acceptance = await _unitOfWork.Repository<Acceptance>().Get(request.AcceptanceDto.AcceptanceId);

            //if (Acceptance is null)
            //    throw new NotFoundException(nameof(Acceptance), request.AcceptanceDto.AcceptanceId);

            ////_mapper.Map(request.AcceptanceDto, Acceptance);

            ///////// File Upload //////////
            /////
            //string uniqueFileName = null;

            //if (request.AcceptanceDto.Doc != null)
            //{

            //    var fileName = Path.GetFileName(request.AcceptanceDto.Doc.FileName);
            //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            //    var a = Directory.GetCurrentDirectory();
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\acceptances", uniqueFileName);

            //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
            //    {
            //        await request.AcceptanceDto.Doc.CopyToAsync(fileSteam);
            //    }
            //}




            //_mapper.Map(request.AcceptanceDto, Acceptance);
            //Acceptance.AcceptanceDate = Acceptance.AcceptanceDate.Value.AddDays(1.0);
            //Acceptance.AcceptanceDocument = request.AcceptanceDto.AcceptanceDocument != null ? "files/acceptances/" + uniqueFileName : Acceptance.AcceptanceDocument.Replace("https://localhost:44395/Content/", String.Empty);

            //await _unitOfWork.Repository<Acceptance>().Update(Acceptance);
            //await _unitOfWork.Save();

            //return Unit.Value;
        }
    }
}
