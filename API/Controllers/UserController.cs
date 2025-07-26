using Business.Users;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using PalMazadStore.Migrations;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly AppDbContext _context;

        public UserController(IUserManager userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(UserModelBo), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<UserModelBo>> CreateUser([FromBody] UserModel userModel)
        {
            var user = await _userManager.CreateUser(userModel);
            return Ok(user);
        }

        [HttpGet]
        [Route("users")]
        [ProducesResponseType(typeof(List<UserModelBo>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{userId:min(1)}/GetById")]
        [ProducesResponseType(typeof(UserModelBo), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userManager.GetUserById(userId);
            return Ok(user);
        }

        [HttpPut]
        [Route("{id:min(1)}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserModel userModel)
        {
            var user = await _userManager.UpdateUser(id, userModel);
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:min(1)}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userManager.DeleteUser(id);
            return NoContent();
        }
    }
}
