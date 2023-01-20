using HitchFix.Models.Dto;

namespace HitchFix.Services
{
    public interface IOrderUpdateService
    {
        Task<OrderDto> UpdateOrder(OrderDto order);
    }
}
