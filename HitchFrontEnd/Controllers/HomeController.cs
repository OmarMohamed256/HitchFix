using HitchFrontEnd.Models;
using HitchFrontEnd.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HitchFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDeviceTypeService _deviceTypeService;
        private readonly IMemoryCache _memoryCache;

        public HomeController(ILogger<HomeController> logger, IDeviceTypeService deviceTypeService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _deviceTypeService = deviceTypeService;
            _memoryCache = memoryCache;
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
                const string deviceTypeID = "_DeviceTypeId";

                HttpContext.Session.SetString(deviceTypeID, device_type.ToString());
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