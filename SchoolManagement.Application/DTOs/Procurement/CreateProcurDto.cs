using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Application.DTOs.Procurement
{
    public class CreateProcurDto
    {
        

        public IFormFile Doc { get; set; }
        public CreateProcurementDto ProcurementForm { get; set; }
}
}
