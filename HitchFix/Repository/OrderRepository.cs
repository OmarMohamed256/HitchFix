using AutoMapper;
using HitchFix.Data;
using HitchFix.Models;
using HitchFix.Models.Dto;
using HitchFix.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HitchFix.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public OrderRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<OrderDto> AddEditOrder(OrderDto orderDto)
        {
            Order order = _mapper.Map<OrderDto, Order>(orderDto);
            foreach (var problem in order.OrderProblems)
            {
                problem.TotalPriceAfterDiscount = ((double)(problem.Price * ((100 - problem.DiscountPrice) / 100)));
            }
            if (order.Id > 0)
            {
                _context.Update(order);
            }
            else
            {
                _context.Add(order);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<Order, OrderDto>(order);
        }

        public async Task<OrderDto> GetOrderById(int orderId)
        {
            Order order = await _context.Orders
                .Include(p => p.OrderProblems)
                .Where(x => x.Id == orderId).FirstOrDefaultAsync();
            return _mapper.Map<Order, OrderDto>(order);
        }

        public async Task<OrderDto> GetOrderByUserId(string UserId)
        {
            Order order = await _context.Orders
               .Include(p => p.OrderProblems)
               .Where(x => x.UserId == UserId).FirstOrDefaultAsync();
            return _mapper.Map<Order, OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            List<Order> orders = await _context.Orders
                .Include(p => p.OrderProblems)
                .ToListAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<bool> RemoveOrder(int orderId)
        {
            try
            {
                Order order = await _context.Orders.FirstOrDefaultAsync(u => u.Id == orderId);
                if (order == null)
                {
                    return false;
                }
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
