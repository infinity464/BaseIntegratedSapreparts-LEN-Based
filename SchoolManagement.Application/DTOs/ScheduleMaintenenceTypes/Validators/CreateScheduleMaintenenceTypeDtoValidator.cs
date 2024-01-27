using FluentValidation;

namespace SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes.Validators
{
    public class CreateScheduleMaintenenceTypeDtoValidator : AbstractValidator<CreateScheduleMaintenenceTypeDto>
    {
        public CreateScheduleMaintenenceTypeDtoValidator()
        {
            Include(new IScheduleMaintenenceTypeDtoValidator());
        }
    }
}
 