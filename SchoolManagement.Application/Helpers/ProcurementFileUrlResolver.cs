using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.Procurement;
using Microsoft.Extensions.Configuration;

namespace SchoolManagement.Application.Helpers
{
    public class ProcurementFileUrlResolver : IValueResolver<Procurement, ProcurementDto, string>
    {

        private readonly IConfiguration _config;
        public ProcurementFileUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Procurement source, ProcurementDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProcurementDocument))
            {

                return _config["ApiUrl"] + source.ProcurementDocument;
            }

            return null;
        }
    }
}
