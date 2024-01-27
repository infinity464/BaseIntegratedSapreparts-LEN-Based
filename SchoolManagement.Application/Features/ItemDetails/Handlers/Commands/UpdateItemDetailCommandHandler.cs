using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.DTOs.ItemDetail.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ItemDetails.Requests.Commands;
using SchoolManagement.Application.Contracts.Persistence;

namespace SchoolManagement.Application.Features.ItemDetails.Handlers.Commands
{
    public class UpdateItemDetailCommandHandler : IRequestHandler<UpdateItemDetailCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateItemDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateItemDetailCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateItemDetailDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.CreateItemDetailDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ItemDetail = await _unitOfWork.Repository<ItemDetail>().Get(request.CreateItemDetailDto.ItemDetailId);

            if (ItemDetail is null)
                throw new NotFoundException(nameof(ItemDetail), request.CreateItemDetailDto.ItemDetailId);

            /////// Image Upload //////////
            
            string uniqueFileName = null;
            
            if (request.CreateItemDetailDto.Photo != null)
            {

                var fileName = Path.GetFileName(request.CreateItemDetailDto.Photo.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Content\\images\\profile", uniqueFileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\item-details", uniqueFileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.CreateItemDetailDto.Photo.CopyToAsync(fileSteam);
                }


            }
            _mapper.Map(request.CreateItemDetailDto, ItemDetail);
            ItemDetail.PhotoUp = request.CreateItemDetailDto.Photo != null ? "files/item-details/" + uniqueFileName : ItemDetail.PhotoUp.Replace("https://localhost:44395/Content/", String.Empty);
            await _unitOfWork.Repository<ItemDetail>().Update(ItemDetail);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
