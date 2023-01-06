using HitchFix.Models.Dto;

namespace HitchFix.Repository.Interfaces
{
    public interface IDeviceProblemRepository
    {
        Task<DeviceProblemDto> AddEditDeviceProblem(DeviceProblemDto deviceProblemDto);
        Task<DeviceProblemDto> GetDeviceProblemById(int deviceProblemId);
        Task<bool> RemoveDeviceProblem(int deviceProblemId);
        Task<IEnumerable<DeviceProblemDto>> GetDeviceProblemsByDeviceId(int deviceId);
    }
}
