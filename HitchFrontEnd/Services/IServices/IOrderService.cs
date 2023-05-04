using HitchFrontEnd.Models;

namespace HitchFrontEnd.Services.IServices
{
    public interface IOrderService
    {
        Task<T> GetAllOrdersAsync<T>(string token = null);
        Task<T> GetOrderByIdAsync<T>(int id, string token = null);
        Task<T> GetOrderByUserIdAsync<T>(int userId, string token = null);
        Task<T> CreateOrderAsync<T>(OrderDto orderDto, string token = null);
        Task<T> UpdateOrderAsync<T>(OrderDto orderDto, string token = null);
        Task<T> DeleteOrderAsync<T>(int id, string token);
    }
}
