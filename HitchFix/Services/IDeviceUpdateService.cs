using HitchFix.Models.Dto;

namespace HitchFix.Services
{
    public interface IDeviceUpdateService
    {
        Task<DeviceDto> UpdateDevice(DeviceDto device);
    }
}
