using AutoMapper;
using HitchFix.Models;
using HitchFix.Models.Dto;
using HitchFix.Repository.Interfaces;

namespace HitchFix.Services
{
    public class OrderUpdateService : IOrderUpdateService
    {
        public IUnitOfWork _unitOfWork { get; }

        public OrderUpdateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OrderDto> UpdateOrder(OrderDto order)
        {
            //var watch = new System.Diagnostics.Stopwatch();

            // watch.Start();
            // should be improved
            IEnumerable<OrderProblemDto> orderProblems = await _unitOfWork.OrderProblemRepository
                .GetOrderProblemsByOrderId(order.Id);
            foreach (var problem in orderProblems)
            {
                await _unitOfWork.OrderProblemRepository.RemoveOrderProblem(problem.OrderId);
            }
            //foreach(var newProblem in device.DeviceProblems)
            //{
            //    await _unitOfWork.DeviceProblemRepository.AddEditDeviceProblem(newProblem);
            //}

            OrderDto newOrder = await _unitOfWork.OrderRepository.AddEditOrder(order);
            // watch.Stop();
            // System.Diagnostics.Debug.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            return newOrder;
        }
    }
}
