using AutoMapper;
using HitchFix.Models;
using HitchFix.Models.Dto;
using HitchFix.Repository.Interfaces;
using HitchFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HitchFix.Controllers
{
    [Route("api/device")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceUpdateService _deviceUpdateService;
        protected ResponseDto _response;
        public IUnitOfWork _unitOfWork { get; }

        public DeviceController(IUnitOfWork unitOfWork, IMapper mapper, IDeviceUpdateService deviceUpdateService)
        {
            this._response = new ResponseDto();
            _unitOfWork = unitOfWork;
            _deviceUpdateService = deviceUpdateService;
        }
        [HttpGet]
        public async Task<object> GetDevices()
        {
            try
            {
                IEnumerable<DeviceDto> list = await _unitOfWork.DeviceRepository.GetDevices();
                _response.Result = list;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        [Route("list/{deviceTypeId}")]
        public async Task<object> GetDevicesByDeviceTypeId(int deviceTypeId)
        {
            try
            {
                IEnumerable<DeviceDto> list = await _unitOfWork.DeviceRepository.GetDevicesByDeviceTypeId(deviceTypeId);
                _response.Result = list;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpGet]
        [Route("{deviceId}")]
        public async Task<object> GetDeviceById(int deviceId)
        {
            try
            {
                DeviceDto deviceDto = await _unitOfWork.DeviceRepository.GetDeviceById(deviceId);
                _response.Result = deviceDto;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<object> AddDevice([FromBody] DeviceDto device)
        {
            try
            {
                DeviceDto newDevice = await _unitOfWork.DeviceRepository.AddEditDevice(device);
                _response.Result = newDevice;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<object> EditDevice([FromBody] DeviceDto device)
        {
            try
            {
                DeviceDto newDevice =  await _deviceUpdateService.UpdateDevice(device);
                _response.Result = newDevice;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Route("{deviceId}")]
        [Authorize(Roles = "admin")]
        public async Task<object> DeleteDevice(int deviceId)
        {
            try
            {
                bool isSucess = await _unitOfWork.DeviceRepository.RemoveDevice(deviceId);
                _response.Result = isSucess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("deviceProblems/{deviceId}")]
        public async Task<object> GetProblemsForADevice(int deviceId)
        {
            try
            {
                List<DeviceProblemDto> list = (List<DeviceProblemDto>) await _unitOfWork.DeviceProblemRepository
                    .GetDeviceProblemsByDeviceId(deviceId);
                _response.Result = list;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("add-list-of-problems")]
        [Authorize(Roles = "admin")]
        public async Task<object> AddListOfProblemsToADevice([FromBody] IEnumerable<DeviceProblemDto> problems)
        {
            try
            {
                List<DeviceProblemDto> newDeviceProblems = (List<DeviceProblemDto>)await _unitOfWork.DeviceProblemRepository
                    .AddListOfProblemsToADevice(problems);
                _response.Result = newDeviceProblems;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("add-problem")]
        [Authorize(Roles = "admin")]
        public async Task<object> AddProblemToADevice([FromBody]DeviceProblemDto deviceProblemDto)
        {
            try
            {
                DeviceProblemDto newDeviceProblem = await _unitOfWork.DeviceProblemRepository
                    .AddEditDeviceProblem(deviceProblemDto);
                _response.Result = newDeviceProblem;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("edit-problem")]
        [Authorize(Roles = "admin")]
        public async Task<object> EditProblemOfADevice([FromBody] DeviceProblemDto deviceProblemDto)
        {
            try
            {
                DeviceProblemDto newDeviceProblem = await _unitOfWork.DeviceProblemRepository
                    .AddEditDeviceProblem(deviceProblemDto);
                _response.Result = newDeviceProblem;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("delete-problem/{problemId}")]
        [Authorize(Roles = "admin")]
        public async Task<object> RemoveProblemFromADevice(int problemId)
        {
            try
            {
                bool isSuccess = await _unitOfWork.DeviceProblemRepository .RemoveDeviceProblem(problemId);
                _response.Result = isSuccess;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
