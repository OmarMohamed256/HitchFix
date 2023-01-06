using HitchFrontEnd.Models;
using HitchFrontEnd.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace HitchFrontEnd.Controllers
{
    public class DeviceTypesController : Controller
    {
        private readonly IDeviceTypeService _deviceTypeService;
        public DeviceTypesController(IDeviceTypeService deviceTypeService)
        {
            _deviceTypeService = deviceTypeService;
        }
        public async Task<IActionResult> DeviceTypesIndex()
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeviceTypesCreate()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles="admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeviceTypesCreate(DeviceTypeDto deviceTypeDto)
        {
            if(ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _deviceTypeService.CreateDeviceTypeAsync<ResponseDto>(deviceTypeDto, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(DeviceTypesIndex));
                }
            }
            return View(deviceTypeDto);
        }
        public async Task<IActionResult> DeviceTypesUpdate(int deviceTypeId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _deviceTypeService.GetDeviceTypeByIdAsync<ResponseDto>(deviceTypeId, accessToken);
            if (response != null && response.IsSuccess)
            {
                DeviceTypeDto model = JsonConvert.DeserializeObject<DeviceTypeDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeviceTypesUpdate(DeviceTypeDto deviceTypeDto)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _deviceTypeService.UpdateDeviceTypeAsync<ResponseDto>(deviceTypeDto, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(DeviceTypesIndex));
                }
            }
            return View(deviceTypeDto);
        }
        public async Task<IActionResult> DeviceTypesDelete(int deviceTypeId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _deviceTypeService.GetDeviceTypeByIdAsync<ResponseDto>(deviceTypeId, accessToken);
            if (response != null && response.IsSuccess)
            {
                DeviceTypeDto model = JsonConvert.DeserializeObject<DeviceTypeDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeviceTypesDelete(DeviceTypeDto deviceTypeDto)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _deviceTypeService.DeleteDeviceTypeAsync<ResponseDto>(deviceTypeDto.Id, accessToken);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(DeviceTypesIndex));
                }
            }
            return View(deviceTypeDto);
        }
    }
}
