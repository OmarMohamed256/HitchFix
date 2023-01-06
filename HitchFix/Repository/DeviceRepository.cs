using AutoMapper;
using HitchFix.Data;
using HitchFix.Models;
using HitchFix.Models.Dto;
using HitchFix.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HitchFix.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public DeviceRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<DeviceDto> AddEditDevice(DeviceDto deviceDto)
        {
            Device device = _mapper.Map<Device>(deviceDto);
            if(device.Id >0)
            {
                _context.Update(device);
            }
            else
            {
                _context.Add(device);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<Device ,DeviceDto>(device);
        }

        public async Task<DeviceDto> GetDeviceById(int deviceId)
        {
            Device device = await _context.Devices.Where(x => x.Id == deviceId).FirstOrDefaultAsync();
            return _mapper.Map<Device, DeviceDto>(device);
        }

        public async Task<IEnumerable<DeviceDto>> GetDevices()
        {
            List<Device> devices = await _context.Devices.ToListAsync();
            return _mapper.Map<List<DeviceDto>>(devices);
        }

        public async Task<bool> RemoveDevice(int deviceId)
        {
            try
            {
                Device device = await _context.Devices.FirstOrDefaultAsync(u => u.Id == deviceId);
                if (device == null)
                {
                    return false;
                }
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
