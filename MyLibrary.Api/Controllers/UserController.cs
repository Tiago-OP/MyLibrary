using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.Api.Dtos;
using MyLibrary.Api.Services;
using MyLibrary.Api.Tests;
using System.Net;

namespace MyLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateUserRequest request)
        {
            var newUser = new User()
            {
                Email = request.Email,
                Name = request.Name,
            };
            var id = _userService.Create(newUser);
            return CreatedAtAction(nameof(GetById), new { id = id }, newUser);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public ActionResult<UserDto> GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user is null) return NotFound();
            return user.ToDto();
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            var users = _userService.GetAll().Select(x => x.ToDto());
            return users.ToList();
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<UserDto> Update([FromBody] UserDto user)
        {
            var u = _userService.GetById(user.Id);
            if (u is null)
                return NotFound();
            _userService.Update(user.ToUser());

            return CreatedAtAction(nameof(GetById), new { Id = user.Id }, user);
        }

        [HttpDelete]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public ActionResult Delete(int id)
        {
            return  _userService.Delete(id) ? NoContent() : NotFound();
        }

    }
}
