using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.SpareRequisitions.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SpareRequisitions.Handlers.Commands
{
    public class DeleteSpareRequisitionCommandHandler : IRequestHandler<DeleteSpareRequisitionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSpareRequisitionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSpareRequisitionCommand request, CancellationToken cancellationToken)
        {
            var SpareRequisition = await _unitOfWork.Repository<SpareRequisition>().Get(request.SpareRequisitionId);

            if (SpareRequisition == null)
                throw new NotFoundException(nameof(SpareRequisition), request.SpareRequisitionId);

            await _unitOfWork.Repository<SpareRequisition>().Delete(SpareRequisition);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
