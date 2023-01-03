using HitchFrontEnd.Models;
using HitchFrontEnd.Services;
using HitchFrontEnd.Services.IServices;

namespace HitchFrontEnd.Services
{
    public class DeviceTypeService : BaseService, IDeviceTypeService
    {
        private readonly IHttpClientFactory _clientFactory;
        public DeviceTypeService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> CreateDeviceTypeAsync<T>(DeviceTypeDto deviceTypeDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = deviceTypeDto,
                Url = SD.HitchFixBase + "api/deviceType",
                AccessToken = token
            });
        }

        public async Task<T> DeleteDeviceTypeAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Delete,
                Url = SD.HitchFixBase + "api/deviceType/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllDeviceTypesAsync<T>(string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.HitchFixBase + "api/deviceType",
                AccessToken = token
            });
        }

        public async Task<T> GetDeviceTypeByIdAsync<T>(int id, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.HitchFixBase + "api/deviceType/" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateDeviceTypeAsync<T>(DeviceTypeDto deviceTypeDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data= deviceTypeDto,
                Url = SD.HitchFixBase + "api/deviceType",
                AccessToken = token
            });
        }
    }
}
