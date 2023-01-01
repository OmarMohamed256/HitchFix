using AutoMapper;
using HitchFix.Models.Dto;
using HitchFix.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HitchFix.Controllers
{
    [Route("api/deviceType")]
    public class DeviceTypeController : Controller
    {
        private readonly IMapper _mapper;
        public IUnitOfWork _unitOfWork { get; }

        public DeviceTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> DeviceTypeIndex()
        {
            List<DeviceTypeDto> list = new();
            list = (List<DeviceTypeDto>)await _unitOfWork.DeviceTypeRepository.GetDeviceTypes();
            return View(list);
        }
        [HttpGet("create")]
        public async Task<IActionResult> CreateDeviceType()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDeviceType(DeviceTypeDto model)
        {
            if (ModelState.IsValid)
            {
                model = await _unitOfWork.DeviceTypeRepository.AddEditDeviceType(model);
            }
            return View(model);
        }

        [HttpGet("edit/{deviceTypeId}")]
        public async Task<IActionResult> EditDeviceType(int deviceTypeId)
        {
            var model =  await _unitOfWork.DeviceTypeRepository.GetDeviceTypeById(deviceTypeId);
            if(model == null) {
                return View();
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDeviceType(DeviceTypeDto model)
        {
            if (ModelState.IsValid)
            {
                model = await _unitOfWork.DeviceTypeRepository.AddEditDeviceType(model);
                ViewData["Success"] = "Device Type Updated Successfully";
            }
            else
            {
                TempData["Error"] = "Failed To Update Device Type";
            }
            return View(model);
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDeviceType(int deviceTypeId)
        {
            var deleted = await _unitOfWork.DeviceTypeRepository.RemoveDeviceType(deviceTypeId);
            if (deleted)
            {
                ViewData["Success"] = "Device Type Deleted Successfully";
            }
            else
            {
                TempData["Error"] = "Failed To Delete Device Type";

            }
            return RedirectToAction(nameof(DeviceTypeIndex));
        }

    }
}
