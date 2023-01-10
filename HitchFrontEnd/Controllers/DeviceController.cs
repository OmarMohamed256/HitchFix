using HitchFrontEnd.Models;
using HitchFrontEnd.Services;
using HitchFrontEnd.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HitchFrontEnd.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly IDeviceTypeService _deviceTypeService;
        private List<DeviceTypeDto> _devicesTypeList;

        public DeviceController(IDeviceService deviceService, IDeviceTypeService deviceTypeService)
        {
            _deviceService = deviceService;
            _deviceTypeService = deviceTypeService;
        }
        public async Task<List<DeviceTypeDto>> IntializeDeviceTypes()
        {
            List<DeviceTypeDto> deviceTypesList = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _deviceTypeService.GetAllDeviceTypesAsync<ResponseDto>(accessToken);
            if (response != null && response.IsSuccess)
            {
                deviceTypesList = JsonConvert.DeserializeObject<List<DeviceTypeDto>>(Convert.ToString(response.Result));
            }
            return deviceTypesList;
        }
        public async Task<IActionResult> DeviceIndex()
        {
            List<DeviceDto> deviceList = new();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _deviceService.GetAllDevicesAsync<ResponseDto>(accessToken);
            if (response != null && response.IsSuccess)
            {
                deviceList = JsonConvert.DeserializeObject<List<DeviceDto>>(Convert.ToString(response.Result));
            }
            return View(deviceList);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeviceCreate()
        {
            _devicesTypeList = await IntializeDeviceTypes();
            ViewData["MyData"] = _devicesTypeList;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeviceCreate(DeviceDto deviceDto)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _deviceService.CreateDeviceAsync<ResponseDto>(deviceDto, accessToken);
            if (response != null && response.IsSuccess)
            {
                return Json(Url.Action("DeviceIndex", "Device"));
            }
            _devicesTypeList = await IntializeDeviceTypes();
            ViewData["MyData"] = _devicesTypeList;
            return Json(Url.Action("DeviceCreate", "Device"));
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeviceUpdate(int deviceId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _deviceService.GetDeviceByIdAsync<ResponseDto>(deviceId, accessToken);
            if (response != null && response.IsSuccess)
            {
                DeviceDto model = JsonConvert.DeserializeObject<DeviceDto>(Convert.ToString(response.Result));
                _devicesTypeList = await IntializeDeviceTypes();
                ViewData["MyData"] = _devicesTypeList;
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeviceUpdate(DeviceDto deviceDto)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _deviceService.UpdateDeviceAsync<ResponseDto>(deviceDto, accessToken);
            if (response != null && response.IsSuccess)
            {
                return Json(Url.Action("DeviceIndex", "Device"));
            }
            _devicesTypeList = await IntializeDeviceTypes();
            ViewData["MyData"] = _devicesTypeList;
            return Json(Url.Action("DeviceCreate", "Device"));
        }
        public async Task<IActionResult> DeviceDelete(int deviceId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _deviceService.GetDeviceByIdAsync<ResponseDto>(deviceId, accessToken);
            if (response != null && response.IsSuccess)
            {
                DeviceDto model = JsonConvert.DeserializeObject<DeviceDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeviceDelete(DeviceDto deviceDto)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _deviceService.DeleteDeviceAsync<ResponseDto>(deviceDto.Id, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(DeviceIndex));
            }
            return View(deviceDto);
        }
    }
}
