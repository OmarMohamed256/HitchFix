using AutoMapper;
using HitchFix.Models.Dto;
using HitchFix.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HitchFix.Controllers
{
    [Route("api/deviceType")]
    [Authorize]
    public class DeviceTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        protected ResponseDto _response;
        public IUnitOfWork _unitOfWork { get; }

        public DeviceTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._response = new ResponseDto();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<object> GetDeviceTypes()
        {
            try
            {
                IEnumerable<DeviceTypeDto> list = await _unitOfWork.DeviceTypeRepository.GetDeviceTypes();
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
        [Route("{id}")]
        public async Task<object> GetDeviceById(int id) 
        {
            try
            {
                DeviceTypeDto deviceTypeDto = await _unitOfWork.DeviceTypeRepository.GetDeviceTypeById(id);
                _response.Result = deviceTypeDto;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<object> CreateDeviceType([FromBody] DeviceTypeDto deviceTypeDto)
        {
            try
            {
                DeviceTypeDto newDeviceTypetDto = await _unitOfWork.DeviceTypeRepository.AddEditDeviceType(deviceTypeDto);
                _response.Result = newDeviceTypetDto;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<object> UpdateDeviceType([FromBody] DeviceTypeDto deviceTypeDto)
        {
            try
            {
                DeviceTypeDto newDeviceTypetDto = await _unitOfWork.DeviceTypeRepository.AddEditDeviceType(deviceTypeDto);
                _response.Result = newDeviceTypetDto;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<object> DeleteDeviceType(int id)
        {
            try
            {
                bool isSucess = await _unitOfWork.DeviceTypeRepository.RemoveDeviceType(id);
                _response.Result = isSucess;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }

    }
}
