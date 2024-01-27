using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Commands;
using SchoolManagement.Application.Features.ItemStors.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ItemStors.Handlers.Commands
{
    public class AddingReturnQtyRegisterCommandHandler : IRequestHandler<AddingReturnQtyRegisterCommand,Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public AddingReturnQtyRegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddingReturnQtyRegisterCommand request, CancellationToken cancellationToken)
        {
            var ItemStor = await _unitOfWork.Repository<ItemStor>().Get(request.IssueRegisterDto.ItemStoreId.Value);
            ItemStor.AvailableQty = ItemStor.AvailableQty + request.IssueRegisterDto.ReturnQtyUpdate;
            //if (item.IssueTypeId == 1)
            //{
            //    itemStor.PermanentQty += item.IssueQty;
            //}

            //ty
            if (request.IssueRegisterDto.IssueTypeId == 2)
            {
                ItemStor.TyQty = ItemStor.TyQty - request.IssueRegisterDto.ReturnQtyUpdate;
            }
            //survey
            if (request.IssueRegisterDto.IssueTypeId == 3)
            {
                ItemStor.SurveyQty = ItemStor.SurveyQty - request.IssueRegisterDto.ReturnQtyUpdate;
            }
            //repair
            if (request.IssueRegisterDto.IssueTypeId == 4)
            {
                ItemStor.RepairQty = ItemStor.RepairQty - request.IssueRegisterDto.ReturnQtyUpdate;
            }
            //shift fitted 
            if (request.IssueRegisterDto.IssueTypeId == 8)
            {
                ItemStor.ShipFittedQty = ItemStor.ShipFittedQty - request.IssueRegisterDto.ReturnQtyUpdate;
            }


            var IssueRegister = await _unitOfWork.Repository<IssueRegister>().Get(request.IssueRegisterDto.IssueRegisterId);
            IssueRegister.ReturnQty = IssueRegister.ReturnQty + request.IssueRegisterDto.ReturnQtyUpdate;

            //_mapper.Map(request.IssueRegisterDto, ItemStor);

            await _unitOfWork.Repository<ItemStor>().Update(ItemStor);
            await _unitOfWork.Save();

            await _unitOfWork.Repository<IssueRegister>().Update(IssueRegister);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
