using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Helpers
{
    public class CorrectiveMaintenenceFileUrlResolver : IValueResolver<CorrectiveMaintenence, CorrectiveMaintenenceDto, string>
    {

        private readonly IConfiguration _config;
        public CorrectiveMaintenenceFileUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(CorrectiveMaintenence source, CorrectiveMaintenenceDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.RefDocument))
            {

                return _config["ApiUrl"] + source.RefDocument;
            }

            return null;
        }
    }
}
