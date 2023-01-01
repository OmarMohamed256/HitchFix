namespace HitchFix.Repository
{
    public interface IUnitOfWork
    {
        IDeviceTypeRepository DeviceTypeRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
