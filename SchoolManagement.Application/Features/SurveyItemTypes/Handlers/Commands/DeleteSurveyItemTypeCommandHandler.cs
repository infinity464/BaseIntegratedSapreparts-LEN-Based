using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.SurveyItemTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Handlers.Commands
{
    public class DeleteSurveyItemTypeCommandHandler : IRequestHandler<DeleteSurveyItemTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSurveyItemTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSurveyItemTypeCommand request, CancellationToken cancellationToken)
        {
            var SurveyItemType = await _unitOfWork.Repository<SurveyItemType>().Get(request.SurveyItemTypeId);

            if (SurveyItemType == null)
                throw new NotFoundException(nameof(SurveyItemType), request.SurveyItemTypeId);

            await _unitOfWork.Repository<SurveyItemType>().Delete(SurveyItemType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
