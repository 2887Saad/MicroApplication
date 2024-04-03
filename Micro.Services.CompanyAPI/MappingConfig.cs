using AutoMapper;
using Micro.Services.CompanyAPI.Models.Dto;
using Micro.Services.CompanyAPI.Models;

namespace Micro.Services.CompanyAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(
                config => {
                    config.CreateMap<CompanyDto, Company>().ReverseMap();
                });
            return mappingConfig;
        }
    }
}
