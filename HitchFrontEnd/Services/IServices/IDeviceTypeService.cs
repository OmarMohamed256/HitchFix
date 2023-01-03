using HitchFrontEnd.Models;

namespace HitchFrontEnd.Services.IServices
{
    public interface IDeviceTypeService : IBaseService
    {
        Task<T> GetAllDeviceTypesAsync<T>(string token = null);
        Task<T> GetDeviceTypeByIdAsync<T>(int id, string token = null);
        Task<T> CreateDeviceTypeAsync<T>(DeviceTypeDto deviceTypeDto, string token = null);
        Task<T> UpdateDeviceTypeAsync<T>(DeviceTypeDto deviceTypeDto, string token = null);
        Task<T> DeleteDeviceTypeAsync<T>(int id, string token);
    }
}
