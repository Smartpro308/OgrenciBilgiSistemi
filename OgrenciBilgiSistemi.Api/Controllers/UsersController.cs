using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OgrenciBilgiSistemi.Business.Interface;
using OgrenciBilgiSistemi.Entity.Dtos.User.Request;

namespace OgrenciBilgiSistemi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //endpoint - api
        [HttpGet("getById")]
        public IActionResult GetById(int userId)
        {
            var result = _userService.GetById(userId);
            return Ok(result);
        }

        //endpoint - api
        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            var result = _userService.GetList();
            return Ok(result);
        }

        [HttpPut("update")]
        public IActionResult Update(UserRequestDto userRequestDto)
        {
            var result = _userService.Update(userRequestDto);
            return Ok(result);
        }

    }
}
