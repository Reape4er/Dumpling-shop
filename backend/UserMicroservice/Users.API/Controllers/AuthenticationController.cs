using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.API.Models;
using Users.API.Services;

namespace Users.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService) {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<DtoAuthenticationResponse>> AuthenticationUser(DtoAuthenticationRequest userRequest)
        {
            var user = await _userService.UserAuthentication(userRequest);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }

}
