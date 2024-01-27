using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Handlers.Commands
{
    public class DeleteMaintenanceAuthorityCommandHandler : IRequestHandler<DeleteMaintenanceAuthorityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteMaintenanceAuthorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteMaintenanceAuthorityCommand request, CancellationToken cancellationToken)
        {
            var MaintenanceAuthority = await _unitOfWork.Repository<MaintenanceAuthority>().Get(request.MaintenanceAuthorityId);

            if (MaintenanceAuthority == null)
                throw new NotFoundException(nameof(MaintenanceAuthority), request.MaintenanceAuthorityId);

            await _unitOfWork.Repository<MaintenanceAuthority>().Delete(MaintenanceAuthority);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
