using HitchFrontEnd.Models;
using HitchFrontEnd.Services.IServices;

namespace HitchFrontEnd.Services
{
    public class DeviceService : BaseService, IDeviceService
    {
        private readonly IHttpClientFactory _clientFactory;
        public DeviceService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public Task<T> GetAllDevicesAsync<T>(string token = null)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.HitchFixBase + "api/device",
                AccessToken= token
            });
        }
        public Task<T> GetAllDevicesByDeviceTypeAsync<T>(int deviceTypeId, string token = null)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.HitchFixBase + "api/device/list/" + deviceTypeId,
                AccessToken = token
            });
        }
        public Task<T> GetDeviceByIdAsync<T>(int id, string token = null)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.HitchFixBase + "api/device/" + id,
                AccessToken = token
            });
        }

        public Task<T> CreateDeviceAsync<T>(DeviceDto deviceDto, string token = null)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.HitchFixBase + "api/device",
                Data = deviceDto,
                AccessToken = token
            });
        }
        public Task<T> UpdateDeviceAsync<T>(DeviceDto deviceDto, string token = null)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.HitchFixBase + "api/device",
                Data = deviceDto,
                AccessToken = token
            });
        }

        public Task<T> DeleteDeviceAsync<T>(int id, string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Delete,
                Url = SD.HitchFixBase + "api/device/" + id,
                AccessToken = token
            });
        }
        public Task<T> GetProblemsForADevice<T>(int id, string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.HitchFixBase + "api/device/deviceProblems/" + id,
                AccessToken = token
            });
        }
        public Task<T> AddProblemToADevice<T>(DeviceProblemDto deviceProblemDto, string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.HitchFixBase + "api/device/add-problem/",
                Data= deviceProblemDto,
                AccessToken = token
            });
        }

        public async Task<T> AddListOfProblemsToADeviceAsync<T>(ICollection<DeviceProblemDto> problems, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = problems,
                Url = SD.HitchFixBase + "api/device/add-list-of-problems/",
                AccessToken = token
            });
        }

        public Task<T> EditProblemOfADevice<T>(DeviceProblemDto deviceProblemDto, string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.HitchFixBase + "api/device/edit-problem/",
                Data = deviceProblemDto,
                AccessToken = token
            });
        }

        public Task<T> RemoveProblemFromADevice<T>(int problemId, string token)
        {
            return this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Delete,
                Url = SD.HitchFixBase + "api/device/delete-problem/" + problemId,
                AccessToken = token
            });
        }
    }
}
