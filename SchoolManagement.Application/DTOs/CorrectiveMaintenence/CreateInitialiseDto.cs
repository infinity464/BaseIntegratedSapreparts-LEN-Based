using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence;

namespace SchoolManagement.Application.DTOs.CorrectiveMaintenence
{
    public class CreateInitialiseDto
    {
        public IFormFile Doc { get; set; }
        public CreateCorrectiveMaintenenceDto CorrectiveMaintenenceForm { get; set; }
    }
}
