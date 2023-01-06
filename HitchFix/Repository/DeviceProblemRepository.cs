using AutoMapper;
using HitchFix.Data;
using HitchFix.Models;
using HitchFix.Models.Dto;
using HitchFix.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HitchFix.Repository
{
    public class DeviceProblemRepository : IDeviceProblemRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public DeviceProblemRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<DeviceProblemDto> AddEditDeviceProblem(DeviceProblemDto deviceProblemDto)
        {
            DeviceProblem deviceProblem = _mapper.Map<DeviceProblemDto, DeviceProblem>(deviceProblemDto);
            deviceProblem.TotalPriceAfterDiscount = ((double)(deviceProblem.Price * ((100 - deviceProblem.DiscountPrice)/100)));
            if(deviceProblem.Id > 0)
            {
                _context.DeviceProblems.Update(deviceProblem);
            }
            else
            {
                _context.DeviceProblems.Add(deviceProblem);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<DeviceProblem, DeviceProblemDto>(deviceProblem);
        }

        public async Task<DeviceProblemDto> GetDeviceProblemById(int deviceProblemId)
        {
            
            DeviceProblem deviceProblem = await _context.DeviceProblems.Where(d => d.Id == deviceProblemId).FirstOrDefaultAsync();
            return _mapper.Map<DeviceProblem, DeviceProblemDto>(deviceProblem);
        }

        public async Task<IEnumerable<DeviceProblemDto>> GetDeviceProblemsByDeviceId(int deviceId)
        {
            List<DeviceProblem> deviceProblems = await _context.DeviceProblems
                .Where(x => x.DeviceId == deviceId).ToListAsync();
            return _mapper.Map<List<DeviceProblemDto>>(deviceProblems);
        }

        public async Task<bool> RemoveDeviceProblem(int deviceProblemId)
        {
            try 
            { 
            DeviceProblem deviceproblem = await _context.DeviceProblems.
                Where(d => d.Id == deviceProblemId).FirstOrDefaultAsync();
            if (deviceproblem == null)
            {
                return false;
            }
            _context.DeviceProblems.Remove(deviceproblem);
            await _context.SaveChangesAsync();
            return true;
            } catch(Exception ex)
            {
                return false;
            }

        }
    }
}
