namespace HitchFix.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IDeviceTypeRepository DeviceTypeRepository { get; }
        IDeviceProblemRepository DeviceProblemRepository { get; }
        IDeviceRepository DeviceRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
