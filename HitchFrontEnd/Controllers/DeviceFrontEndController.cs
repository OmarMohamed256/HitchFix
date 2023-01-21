using HitchFrontEnd.Services;
using HitchFrontEnd.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HitchFrontEnd.Controllers
{
    public class DeviceFrontEndController : Controller
    {
        private readonly IDeviceService _deviceService;
        public DeviceFrontEndController(IDeviceService deviceService, IDeviceTypeService deviceTypeService)
        {
            _deviceService = deviceService;
        }
        public async Task<IActionResult> DeviceFrontEndIndex()
        {
            string key = "DeviceTypeId";
            int deviceTypeId = Int16.Parse(Request.Cookies[key]);

            return View();
        }
    }
}
