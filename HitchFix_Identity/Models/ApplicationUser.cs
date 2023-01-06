using Microsoft.AspNetCore.Identity;

namespace HitchFix_Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
