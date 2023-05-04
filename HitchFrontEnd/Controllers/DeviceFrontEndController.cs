using HitchFrontEnd.Models;
using HitchFrontEnd.Models.SessionModel;
using HitchFrontEnd.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace HitchFrontEnd.Controllers
{
    public class DeviceFrontEndController : Controller
    {
        private readonly IDeviceService _deviceService;
        public DeviceFrontEndController(IDeviceService deviceService, IDeviceTypeService deviceTypeService, IMemoryCache memoryCache)
        {
            _deviceService = deviceService;
        }
        public async Task<IActionResult> DeviceFrontEndIndex()
        {
            const string deviceTypeID = "_DeviceTypeId";
            int deviceTypeId = Int32.Parse(HttpContext.Session.GetString(deviceTypeID));
            List<DeviceDto> deviceList = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _deviceService.GetAllDevicesByDeviceTypeAsync<ResponseDto>(deviceTypeId, accessToken);
            if (response != null && response.IsSuccess)
            {
                deviceList = JsonConvert.DeserializeObject<List<DeviceDto>>(Convert.ToString(response.Result));
            }
            return View(deviceList);
        }
        public async Task<IActionResult> ShowProblems(IFormCollection form)
        {
            var device_data = form["device_data"];
            string x = device_data[0];

            if (!device_data.IsNullOrEmpty()) {
                string deviceJson = x;
                HttpContext.Session.SetString("Device", deviceJson);
                return RedirectToAction("DeviceProblemFrontEndIndex", "DeviceProblemFrontEnd");
            }
            return View();
        }
    }
}
