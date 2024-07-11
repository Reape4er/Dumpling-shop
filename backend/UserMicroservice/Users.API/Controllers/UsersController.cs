using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Users.API.Attributes;
using Users.API.Models;
using Users.API.Services;
using Users.db.Entities;

namespace Users.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _automapper;

        public UsersController(IUserService userService, IMapper automapper)
        {
            _userService = userService;
            _automapper = automapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DtoUser>>> GetUsersAsync()
        {
            return await _userService.GetUsersAsync();
        }

        [ActionName("GetUserByIdAsync")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DtoUser>> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var user = await _userService.GetUserById(id);
            
            if (user == null) {
                return NotFound();
            }
            return _automapper.Map<DtoUser>(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<DtoUser?>> CreateUserAsync(DtoUserRegistration user)
        {
            var newUser = await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = newUser.Id }, newUser);
        }

        [HttpPut]
        public async Task<ActionResult> PutUserAsync(int id, DtoUser user) {
            //var dbUser = _automapper.Map<DbUser>(user);
            if (user.Id != id) {
                return BadRequest();
            }
            if (!dbUserExists(id).Result)
            {
                return NotFound();
            }
            try
            {
                await _userService.PutUserById(user);
            }
            catch (DbUpdateConcurrencyException) {
                throw;
            }
            return NoContent();
        }


        private async Task<bool> dbUserExists(int id)
        {
            var User = await _userService.GetUserById(id);
            if (User == null)
            {
                return false;
            }
            return true;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            if (!dbUserExists(id).Result)
            {
                return NotFound();
            }
            try
            {
                await _userService.DeleteUserByIdAsync(id);
            }
            catch
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
