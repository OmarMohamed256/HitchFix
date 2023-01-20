using HitchFix.Models.Dto;

namespace HitchFix.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderDto> AddEditOrder(OrderDto orderDto);
        Task<OrderDto> GetOrderByUserId(string UserId);
        Task<OrderDto> GetOrderById(int orderId);
        Task<bool> RemoveOrder(int orderId);
        Task<IEnumerable<OrderDto>> GetOrders();
    }
}
