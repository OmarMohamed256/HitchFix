namespace HitchFix.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IDeviceTypeRepository DeviceTypeRepository { get; }
        IDeviceProblemRepository DeviceProblemRepository { get; }
        IDeviceRepository DeviceRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderProblemRepository OrderProblemRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
