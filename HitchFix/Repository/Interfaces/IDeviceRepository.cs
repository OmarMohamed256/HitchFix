using HitchFix.Models.Dto;

namespace HitchFix.Repository.Interfaces
{
    public interface IDeviceRepository
    {
        Task<DeviceDto> AddEditDevice(DeviceDto deviceDto);
        Task<DeviceDto> GetDeviceById(int deviceId);
        Task<IEnumerable<DeviceDto>> GetDevicesByDeviceTypeId(int deviceTypeId);
        Task<bool> RemoveDevice(int deviceId);
        Task<IEnumerable<DeviceDto>> GetDevices();
    }
}
