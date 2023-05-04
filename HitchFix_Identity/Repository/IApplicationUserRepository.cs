using HitchFix_Identity.Models.Dtos;

namespace HitchFix_Identity.Repository
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUserDto> GetUserByIdAsync(string id);
    }
}
