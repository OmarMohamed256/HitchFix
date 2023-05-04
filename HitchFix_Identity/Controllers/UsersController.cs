using HitchFix_Identity.Models;
using HitchFix_Identity.Models.Dtos;
using HitchFix_Identity.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HitchFix_Identity.Controllers
{
    [Route("identity/users")]
    public class UsersController : ControllerBase
    {
        protected ResponseDto _response;
        public IApplicationUserRepository _appUser { get; }
        public UsersController(IApplicationUserRepository appUser)
        {
            this._response = new ResponseDto();
            _appUser = appUser;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetUserById(string id)
        {
            try
            {
                ApplicationUserDto user = await _appUser.GetUserByIdAsync(id);
                _response.Result = user;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
