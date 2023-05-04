using AutoMapper;
using HitchFix.Data;
using HitchFix.Models;
using HitchFix.Models.Dto;
using HitchFix.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HitchFix.Repository
{
    public class OrderProblemRepository : IOrderProblemRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public OrderProblemRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<OrderProblemDto> AddEditOrderProblem(OrderProblemDto orderProblemDto)
        {
            OrderProblem problem = _mapper.Map<OrderProblemDto, OrderProblem>(orderProblemDto);
            problem.TotalPriceAfterDiscount = ((double)(problem.Price * ((100 - problem.DiscountPrice) / 100)));
            if (problem.Id > 0)
            {
                _context.OrderProblems.Add(problem);
            }
            else
            {
                _context.OrderProblems.Update(problem);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderProblem, OrderProblemDto>(problem);
        }

        public async Task<IEnumerable<OrderProblemDto>> AddListOfProblemsToAnOrder(IEnumerable<OrderProblemDto> problems)
        {
            List<OrderProblem> orderProblems = _mapper.Map<List<OrderProblem>>(problems);
            foreach (var problem in orderProblems)
            {
                problem.TotalPriceAfterDiscount = ((double)(problem.Price * ((100 - problem.DiscountPrice) / 100)));
            }
            _context.OrderProblems.AddRange(orderProblems);
            await _context.SaveChangesAsync();

            return _mapper.Map<List<OrderProblemDto>>(orderProblems);
        }

        public async Task<OrderProblemDto> GetOrderProblemById(int orderProblemId)
        {
            OrderProblem problem = await _context.OrderProblems.FirstOrDefaultAsync(p => p.Id == orderProblemId);
            if(problem == null)
            {
                return null;
            }
            return _mapper.Map<OrderProblem, OrderProblemDto>(problem);
        }

        public async Task<IEnumerable<OrderProblemDto>> GetOrderProblemsByOrderId(int orderId)
        {
            List<OrderProblem> orderProblems = await _context.OrderProblems
                .Where(p => p.OrderId == orderId).ToListAsync();

            return _mapper.Map<List<OrderProblemDto>>(orderProblems);
        }

        public async Task<bool> RemoveOrderProblem(int orderProblemId)
        {
            try
            {
                OrderProblem orderProblem = await _context.OrderProblems.
                    Where(d => d.Id == orderProblemId).FirstOrDefaultAsync();
                if (orderProblem == null)
                {
                    return false;
                }
                _context.OrderProblems.Remove(orderProblem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
