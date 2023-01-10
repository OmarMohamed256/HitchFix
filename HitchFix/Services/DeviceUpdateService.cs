using AutoMapper;
using HitchFix.Models.Dto;
using HitchFix.Repository.Interfaces;

namespace HitchFix.Services
{
    public class DeviceUpdateService : IDeviceUpdateService
    {
        private readonly IMapper _mapper;
        public IUnitOfWork _unitOfWork { get; }

        public DeviceUpdateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DeviceDto> UpdateDevice(DeviceDto device)
        {
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            // should be improved
            IEnumerable<DeviceProblemDto> deviceProblems = await _unitOfWork.DeviceProblemRepository
                .GetDeviceProblemsByDeviceId(device.Id);
            foreach (var problem in deviceProblems)
            {
                await _unitOfWork.DeviceProblemRepository.RemoveDeviceProblem(problem.Id);
            }
            //foreach(var newProblem in device.DeviceProblems)
            //{
            //    await _unitOfWork.DeviceProblemRepository.AddEditDeviceProblem(newProblem);
            //}

            DeviceDto newDevice = await _unitOfWork.DeviceRepository.AddEditDevice(device);
            watch.Stop();
            System.Diagnostics.Debug.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            return newDevice;
        }
    }
}
