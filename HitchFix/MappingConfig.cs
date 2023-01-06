using AutoMapper;
using HitchFix.Models;
using HitchFix.Models.Dto;

namespace HitchFix
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<DeviceTypeDto, DeviceType>().ReverseMap();
                config.CreateMap<DeviceDto, Device>().ReverseMap();
                config.CreateMap<DeviceProblemDto, DeviceProblem>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
