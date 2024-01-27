using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.JobCategorys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.JobCategorys.Handlers.Commands
{
    public class DeleteJobCategoryCommandHandler : IRequestHandler<DeleteJobCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteJobCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteJobCategoryCommand request, CancellationToken cancellationToken)
        {
            var JobCategory = await _unitOfWork.Repository<JobCategory>().Get(request.JobCategoryId);

            if (JobCategory == null)
                throw new NotFoundException(nameof(JobCategory), request.JobCategoryId);

            await _unitOfWork.Repository<JobCategory>().Delete(JobCategory);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
