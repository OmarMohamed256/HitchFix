using HitchFrontEnd.Models;

namespace HitchFrontEnd.Services.IServices
{
    public interface IDeviceService : IBaseService
    {
        Task<T> GetAllDevicesAsync<T>(string token = null);
        Task<T> GetAllDevicesByDeviceTypeAsync<T>(int deviceTypeId, string token = null);
        Task<T> GetDeviceByIdAsync<T>(int id, string token = null);
        Task<T> CreateDeviceAsync<T>(DeviceDto deviceDto, string token = null);
        Task<T> UpdateDeviceAsync<T>(DeviceDto deviceDto, string token = null);
        Task<T> DeleteDeviceAsync<T>(int id, string token);
        Task<T> GetProblemsForADevice<T>(int id, string token);
        Task<T> AddProblemToADevice<T>(DeviceProblemDto deviceProblemDto, string token);
        Task<T> AddListOfProblemsToADeviceAsync<T>(ICollection<DeviceProblemDto> problems, string token);
        Task<T> EditProblemOfADevice<T>(DeviceProblemDto deviceProblemDto, string token);
        Task<T> RemoveProblemFromADevice<T>(int problemId, string token);
    }
}
