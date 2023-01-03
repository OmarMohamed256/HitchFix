using HitchFrontEnd.Models;
using HitchFrontEnd.Services.IServices;
using Microsoft.AspNetCore.Authentication;
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
            var response = await _deviceTypeService.GetAllDeviceTypesAsync<ResponseDto>("");
            if (response != null && response.IsSuccess)
            {
                deviceTypesList = JsonConvert.DeserializeObject<List<DeviceTypeDto>>(Convert.ToString(response.Result));
            }
                return View(deviceTypesList);
        }
        public async Task<IActionResult> DeviceTypesCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeviceTypesCreate(DeviceTypeDto deviceTypeDto)
        {
            if(ModelState.IsValid)
            {
                var response = await _deviceTypeService.CreateDeviceTypeAsync<ResponseDto>(deviceTypeDto, null);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(DeviceTypesIndex));
                }
            }
            return View(deviceTypeDto);
        }
        public async Task<IActionResult> DeviceTypesUpdate(int deviceTypeId)
        {
            var response = await _deviceTypeService.GetDeviceTypeByIdAsync<ResponseDto>(deviceTypeId, null);
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
                var response = await _deviceTypeService.UpdateDeviceTypeAsync<ResponseDto>(deviceTypeDto, null);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(DeviceTypesIndex));
                }
            }
            return View(deviceTypeDto);
        }
        public async Task<IActionResult> DeviceTypesDelete(int deviceTypeId)
        {
            var response = await _deviceTypeService.GetDeviceTypeByIdAsync<ResponseDto>(deviceTypeId, null);
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
                var response = await _deviceTypeService.DeleteDeviceTypeAsync<ResponseDto>(deviceTypeDto.Id, null);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(DeviceTypesIndex));
                }
            }
            return View(deviceTypeDto);
        }
    }
}
