using AutoMapper;
using Micro.Services.CategoryAPI.Models;
using Micro.Services.CategoryAPI.Models.Dto;

namespace Micro.Services.CategoryAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(
                config => {
                    config.CreateMap<CategoryDto, Category>().ReverseMap();
                });
            return mappingConfig;
        }
    }
}
