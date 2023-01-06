using HitchFix.Models;
using HitchFix.Models.Dto;

namespace HitchFix.Repository.Interfaces
{
    public interface IDeviceTypeRepository
    {
        Task<DeviceTypeDto> AddEditDeviceType(DeviceTypeDto deviceTypeDto);
        Task<DeviceTypeDto> GetDeviceTypeById(int deviceTypeId);
        Task<bool> RemoveDeviceType(int deviceTypeId);
        Task<IEnumerable<DeviceTypeDto>> GetDeviceTypes();
    }
}
