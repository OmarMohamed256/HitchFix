using AutoMapper;
using HitchFix.Models.Dto;
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
        public async Task<object> GetOrderById(int orderId)
        {
            try
            {
                OrderDto deviceDto = await _unitOfWork.OrderRepository.GetOrderById(orderId);
                _response.Result = deviceDto;
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
