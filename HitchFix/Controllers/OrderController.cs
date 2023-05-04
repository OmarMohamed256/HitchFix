using AutoMapper;
using HitchFix.Models;
using HitchFix.Models.Dto;
using HitchFix.Repository;
using HitchFix.Repository.Interfaces;
using HitchFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HitchFix.Controllers
{
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        protected ResponseDto _response;
        private readonly IOrderUpdateService _orderUpdateService;
        public IUnitOfWork _unitOfWork { get; }
        public OrderController(IUnitOfWork unitOfWork, IMapper mapper, IOrderUpdateService orderUpdateService)
        {
            this._response = new ResponseDto();
            _unitOfWork = unitOfWork;
            _orderUpdateService = orderUpdateService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<object> GetOrders()
        {
            try
            {
                IEnumerable<OrderDto> list = await _unitOfWork.OrderRepository.GetOrders();
                _response.Result = list;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        [Route("{orderId}")]
        [Authorize]
        public async Task<object> GetOrderById(int orderId)
        {
            try
            {
                OrderDto orderDto = await _unitOfWork.OrderRepository.GetOrderById(orderId);
                _response.Result = orderDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<object> GetOrderByUserId(int userId)
        {
            try
            {
                OrderDto orderDto = await _unitOfWork.OrderRepository.GetOrderByUserId(userId);
                _response.Result = orderDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [Authorize]
        public async Task<object> AddOrder([FromBody] OrderDto order)
        {
            try
            {
                OrderDto newOrder = await _unitOfWork.OrderRepository.AddEditOrder(order);
                _response.Result = newOrder;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<object> EditOrder([FromBody] OrderDto order)
        {
            try
            {
                OrderDto orderDto = await _unitOfWork.OrderRepository.AddEditOrder(order);
                _response.Result = orderDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete]
        [Route("{orderId}")]
        [Authorize(Roles = "admin")]
        public async Task<object> DeleteOrder(int orderId)
        {
            try
            {
                bool isSucess = await _unitOfWork.OrderRepository.RemoveOrder(orderId);
                _response.Result = isSucess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("orderProblems/{orderId}")]
        public async Task<object> GetProblemsForAnOrder(int orderId)
        {
            try
            {
                List<OrderProblemDto> list = (List<OrderProblemDto>)await _unitOfWork.OrderProblemRepository
                    .GetOrderProblemsByOrderId(orderId);
                _response.Result = list;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("add-list-of-problems")]
        [Authorize]
        public async Task<object> AddListOfProblemsToAnOrder([FromBody] IEnumerable<OrderProblemDto> problems)
        {
            try
            {
                List<OrderProblemDto> newOrderProblems = (List<OrderProblemDto>)await _unitOfWork.OrderProblemRepository
                    .AddListOfProblemsToAnOrder(problems);
                _response.Result = newOrderProblems;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("add-problem")]
        [Authorize(Roles = "admin")]
        public async Task<object> AddProblemToAnOrder([FromBody] OrderProblemDto orderProblemDto)
        {
            try
            {
                OrderProblemDto newOrderProblem = await _unitOfWork.OrderProblemRepository
                    .AddEditOrderProblem(orderProblemDto);
                _response.Result = newOrderProblem;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("edit-problem")]
        [Authorize(Roles = "admin")]
        public async Task<object> EditProblemOfAnOrder([FromBody] OrderProblemDto orderProblemDto)
        {
            try
            {
                OrderProblemDto newOrderProblem = await _unitOfWork.OrderProblemRepository
                    .AddEditOrderProblem(orderProblemDto);
                _response.Result = newOrderProblem;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("delete-problem/{problemId}")]
        [Authorize(Roles = "admin")]
        public async Task<object> RemoveProblemFromAnOrder(int problemId)
        {
            try
            {
                bool isSuccess = await _unitOfWork.OrderProblemRepository.RemoveOrderProblem(problemId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }

    }
}
