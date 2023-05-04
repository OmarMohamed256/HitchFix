using HitchFrontEnd.Models;
using HitchFrontEnd.Models.SessionModel;
using HitchFrontEnd.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace HitchFrontEnd.Controllers
{
    public class DeviceProblemFrontEndController : Controller
    {
        private readonly IDeviceService _deviceService;

        public DeviceProblemFrontEndController(IDeviceService deviceService, IDeviceTypeService deviceTypeService, IMemoryCache memoryCache)
        {
            _deviceService = deviceService;
        }
        public async Task<IActionResult> DeviceProblemFrontEndIndex()
        {
            // Retrieve a Device object from session
            var deviceJsonFromSession = HttpContext.Session.GetString("Device");
            var deviceFromSession = JsonConvert.DeserializeObject<DeviceSession>(deviceJsonFromSession);

            int deviceId = Int32.Parse(deviceFromSession.Id);

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            List<DeviceProblemDto> problemsList = new List<DeviceProblemDto>();
            var response = await _deviceService.GetProblemsForADevice<ResponseDto>(deviceId, accessToken);

            if (response != null && response.IsSuccess)
            {
                problemsList = JsonConvert.DeserializeObject<List<DeviceProblemDto>>(Convert.ToString(response.Result));
            }
            return View(problemsList);
        }
        public IActionResult SendToRegister(IFormCollection form)
        {
            var problem_data = form["problem_data"];
            problem_data = '[' + problem_data + ']';
            HttpContext.Session.SetString("problem_data", problem_data.ToString());
            return RedirectToAction("OrderReviewIndex", "OrderReview");
        }
    }
}
