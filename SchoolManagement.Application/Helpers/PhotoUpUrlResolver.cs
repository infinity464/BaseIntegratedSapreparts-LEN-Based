using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.ItemDetail;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Helpers
{
    public class PhotoUpUrlResolver : IValueResolver<ItemDetail, ItemDetailDto, string>
    {

        private readonly IConfiguration _config;
        public PhotoUpUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(ItemDetail source, ItemDetailDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PhotoUp))
            {

                return _config["ApiUrl"] + source.PhotoUp;
            }

            return null;
        }
    }
}
