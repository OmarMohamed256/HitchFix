using AutoMapper;
using HitchFix.Models.Dto;
using HitchFix.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HitchFix.Controllers
{
    [Route("api/device")]
    public class DeviceController : ControllerBase
    {
        private readonly IMapper _mapper;
        protected ResponseDto _response;
        public IUnitOfWork _unitOfWork { get; }

        public DeviceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._response = new ResponseDto();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        [Route("{deviceId}")]
        public async Task<object> GetDevicesById(int deviceId)
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
                DeviceDto newDevice = await _unitOfWork.DeviceRepository.AddEditDevice(device);
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
        [HttpPost("add-problem")]
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
