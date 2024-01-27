using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SpareRequiredStatuss.Handlers.Commands
{
    public class DeleteSpareRequiredStatusCommandHandler : IRequestHandler<DeleteSpareRequiredStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSpareRequiredStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSpareRequiredStatusCommand request, CancellationToken cancellationToken)
        {
            var SpareRequiredStatus = await _unitOfWork.Repository<SpareRequiredStatus>().Get(request.SpareRequiredStatusId);

            if (SpareRequiredStatus == null)
                throw new NotFoundException(nameof(SpareRequiredStatus), request.SpareRequiredStatusId);

            await _unitOfWork.Repository<SpareRequiredStatus>().Delete(SpareRequiredStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
