using AutoMapper;
using HitchFix.Data;
using HitchFix.Models;
using HitchFix.Models.Dto;
using HitchFix.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HitchFix.Repository
{
    public class DeviceTypeRepository : IDeviceTypeRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public DeviceTypeRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<DeviceTypeDto> AddEditDeviceType(DeviceTypeDto deviceTypeDto)
        {
            DeviceType deviceType = _mapper.Map<DeviceTypeDto, DeviceType>(deviceTypeDto);
            if(deviceType.Id > 0)
            {
                _context.DeviceTypes.Update(deviceType);
            }
            else
            {
                _context.DeviceTypes.Add(deviceType);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<DeviceType, DeviceTypeDto>(deviceType);
        }

        public async Task<DeviceTypeDto> GetDeviceTypeById(int deviceTypeId)
        {
            DeviceType deviceType = await _context.DeviceTypes.Where(d => d.Id == deviceTypeId).FirstOrDefaultAsync();
            return _mapper.Map<DeviceType, DeviceTypeDto>(deviceType);
        }

        public async Task<bool> RemoveDeviceType(int deviceTypeId)
        {
            DeviceType deviceType = await _context.DeviceTypes.Where(d => d.Id == deviceTypeId).FirstOrDefaultAsync();
            if(deviceType != null)
            {
                _context.DeviceTypes.Remove(deviceType);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<DeviceTypeDto>> GetDeviceTypes()
        {
            List<DeviceType> devices = await _context.DeviceTypes.ToListAsync();
            return _mapper.Map<List<DeviceTypeDto>>(devices);
        }
    }
}
