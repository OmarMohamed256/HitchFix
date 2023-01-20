using HitchFix.Models.Dto;

namespace HitchFix.Repository.Interfaces
{
    public interface IOrderProblemRepository
    {
        Task<OrderProblemDto> AddEditOrderProblem(OrderProblemDto orderProblemDto);
        Task<OrderProblemDto> GetOrderProblemById(int orderProblemId);
        Task<bool> RemoveOrderProblem(int orderProblemId);
        Task<IEnumerable<OrderProblemDto>> GetOrderProblemsByOrderId(int orderId);
    }
}
