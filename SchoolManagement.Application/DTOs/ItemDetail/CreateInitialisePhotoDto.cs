using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs.ItemDetail;

namespace SchoolManagement.Application.DTOs.ItemDetail
{
    public class CreateInitialisePhotoDto
    {
        public IFormFile Photo { get; set; }
        public CreateItemDetailDto ItemDetailForm { get; set; }
    }
}
