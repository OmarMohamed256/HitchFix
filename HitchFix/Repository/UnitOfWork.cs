using AutoMapper;
using HitchFix.Data;
using HitchFix.Repository.Interfaces;

namespace HitchFix.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public IDeviceTypeRepository DeviceTypeRepository => new DeviceTypeRepository(_context, _mapper);
        public IDeviceProblemRepository DeviceProblemRepository => new DeviceProblemRepository(_context, _mapper);
        public IDeviceRepository DeviceRepository => new DeviceRepository(_context, _mapper);
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
