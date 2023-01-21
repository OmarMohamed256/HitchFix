using HitchFrontEnd.Models;
using HitchFrontEnd.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HitchFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDeviceTypeService _deviceTypeService;

        public HomeController(ILogger<HomeController> logger, IDeviceTypeService deviceTypeService)
        {
            _logger = logger;
            _deviceTypeService = deviceTypeService;
        }

        public async Task<IActionResult> Index()
        {
            List<DeviceTypeDto> deviceTypesList = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _deviceTypeService.GetAllDeviceTypesAsync<ResponseDto>(accessToken);
            if (response != null && response.IsSuccess)
            {
                deviceTypesList = JsonConvert.DeserializeObject<List<DeviceTypeDto>>(Convert.ToString(response.Result));
            }
            return View(deviceTypesList);
        }
        public async Task<IActionResult> SendToDevices(int device_type)
        {
            if (device_type != null)
            {
                string key = "DeviceTypeId";
                int value = device_type;
                CookieOptions co = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7),
                };
                Response.Cookies.Append(key, value.ToString(), co);

                return RedirectToAction("DeviceFrontEndIndex", "DeviceFrontEnd");
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> LoginAsync()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            SignOut("Cookies", "oidc");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}