using HitchFix_Identity.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace HitchFix_Identity.Pages.Account.Register
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IndexModel(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<IdentityRole> roleInManager
    )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleInManager;
        }
        [BindProperty]
        public RegisterViewModel Input { get; set; }


        public async Task<IActionResult> OnGet(string returnUrl)
        {
            List<string> roles = new()
            {
                SD.Admin,
                SD.Customer
            };
            ViewData["roles_message"] = roles;
            Input = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return Page();
        }
        public async Task<IActionResult> OnPost(string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    EmailConfirmed = true,
                    Name= Input.Name,
                    PhoneNumber = Input.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(Input.RoleName).GetAwaiter().GetResult())
                    {
                        var userRole = new IdentityRole()
                        {
                            Name = Input.RoleName,
                            NormalizedName= Input.RoleName
                        };
                        await _roleManager.CreateAsync(userRole);
                    }
                    await _userManager.AddToRoleAsync(user, Input.RoleName);

                    await _userManager.AddClaimsAsync(user, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, Input.Email),
                        new Claim(JwtClaimTypes.Email, Input.Email),
                        new Claim(JwtClaimTypes.Role, Input.RoleName),
                    });

                    var loginResult = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: true);

                    if (loginResult.Succeeded)
                    {
                        if (Url.IsLocalUrl(Input.ReturnUrl))
                        {
                            return Redirect(Input.ReturnUrl);
                        }
                        else if (string.IsNullOrEmpty(Input.ReturnUrl))
                        {
                            return Redirect("~/");
                        }
                        else
                        {
                            throw new Exception("invalid return url");
                        }
                    }
                }
            }
            return Page();
        }
    }
}
