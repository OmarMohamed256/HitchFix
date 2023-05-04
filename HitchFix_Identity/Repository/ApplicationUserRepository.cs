using HitchFix_Identity.Data;
using HitchFix_Identity.Models;
using HitchFix_Identity.Models.Dtos;
using System;

namespace HitchFix_Identity.Repository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        public ApplicationDbContext _context { get; }
        public ApplicationUserRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<ApplicationUserDto> GetUserByIdAsync(string id)
        {
            ApplicationUser user = await _context.Users.FindAsync(id);
            return new ApplicationUserDto()
            {
                Email= user.Email,
                Id= id,
                Name= user.Name,
                PhoneNumber= user.PhoneNumber,
                UserName = user.UserName,
            };
        }
    }
}
