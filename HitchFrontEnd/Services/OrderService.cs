using HitchFrontEnd.Models;
using HitchFrontEnd.Services.IServices;

namespace HitchFrontEnd.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IHttpClientFactory _clientFactory;
        public OrderService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> CreateOrderAsync<T>(OrderDto orderDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = orderDto,
                Url = SD.HitchFixBase + "api/order",
                AccessToken = token
            });
        }

        public async Task<T> DeleteOrderAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Delete,
                Url = SD.HitchFixBase + "api/order/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllOrdersAsync<T>(string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.HitchFixBase + "api/order",
                AccessToken = token
            });
        }

        public async Task<T> GetOrderByIdAsync<T>(int id, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.HitchFixBase + "api/order/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetOrderByUserIdAsync<T>(int userId, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.Get,
                Url = SD.HitchFixBase + "api/order/user/" + userId,
                AccessToken = token
            });
        }

        public async Task<T> UpdateOrderAsync<T>(OrderDto orderDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = orderDto,
                Url = SD.HitchFixBase + "api/order",
                AccessToken = token
            });
        }

    }
}
