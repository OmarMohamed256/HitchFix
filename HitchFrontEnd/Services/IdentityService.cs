using HitchFrontEnd.Models;
using HitchFrontEnd.Services.IServices;

namespace HitchFrontEnd.Services
{
    public class IdentityService : BaseService, IIdentityService
    {
        private readonly IHttpClientFactory _clientFactory;
        public IdentityService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetUserByUserNameAsync<T>(string userId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.HitchFixIdentityBase + "identity/users/" + userId,
                AccessToken = token
            });
        }
    }
}
