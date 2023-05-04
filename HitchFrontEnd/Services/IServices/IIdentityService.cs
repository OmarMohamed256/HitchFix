namespace HitchFrontEnd.Services.IServices
{
    public interface IIdentityService : IBaseService
    {
        Task<T> GetUserByUserNameAsync<T>(string userId, string token = null);
    }
}
