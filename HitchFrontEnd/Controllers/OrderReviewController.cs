using HitchFrontEnd.Models;
using HitchFrontEnd.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using HitchFrontEnd.Models.SessionModel;

namespace HitchFrontEnd.Controllers
{
    public class OrderReviewController : Controller
    {
        private readonly IIdentityService _identityService;
        public OrderReviewController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [Authorize]
        public async Task<IActionResult> OrderReviewIndex()
        {
            OrderUserDataDto userData = new OrderUserDataDto();
            var userId = User.Claims.Where(u => u.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;
            if(userId != null)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _identityService.GetUserByUserNameAsync<ResponseDto>(userId, accessToken);
                if(response != null && response.IsSuccess)
                {
                    userData = JsonConvert.DeserializeObject<OrderUserDataDto>(Convert.ToString(response.Result));
                }
            }
            return View(userData);
        }
        [Authorize]
        public async Task<IActionResult> ReviewOrder()
        {
            //show problems
            var problem_data = HttpContext.Session.GetString("problem_data");
            List<DeviceProblemDto> deviceProblems =
                JsonConvert.DeserializeObject<List<DeviceProblemDto>>(problem_data);
            return View(deviceProblems);
        }
        [Authorize]
        public async Task<IActionResult> CreateOrder(IFormCollection form)
        {
            //create order and redirect to thankyou page

            //problems
            var problem_data = HttpContext.Session.GetString("problem_data");
            List<OrderProblemDto> orderProblems =
                JsonConvert.DeserializeObject<List<OrderProblemDto>>(problem_data);
            //user data
            OrderUserDataDto userData = new OrderUserDataDto();
            var userId = User.Claims.Where(u => u.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;
            if (userId != null)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _identityService.GetUserByUserNameAsync<ResponseDto>(userId, accessToken);
                if (response != null && response.IsSuccess)
                {
                    userData = JsonConvert.DeserializeObject<OrderUserDataDto>(Convert.ToString(response.Result));
                }
            }

            OrderDto order = new OrderDto()
            {
                DeviceId = Int32.Parse(HttpContext.Session.GetString("_DeviceTypeId")),
                UserDataDto = userData,
                OrderStatus = "pending",
                OrderTime = DateTime.Now,
            };

            return View();
        }
    }
}
