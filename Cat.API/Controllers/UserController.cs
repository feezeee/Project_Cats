using AutoMapper;
using BLL.Services;
using Cat.API.Request;
using Cat.API.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAccountService _userService;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IAccountService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;

        }
        
        [Route("new")]
        [HttpPost]
        public IActionResult Post([FromBody] PostUserRequest user)
        {
            try
            {
                _userService.Create(_mapper.Map<BLL.Entities.Account>(user));
                return Ok("Пользователь добавлен! добавлен )");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("get")]
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                var users = _mapper.Map<IEnumerable<GetAccountResponse>>(_userService.Get());
                if (users == null)
                {
                    return NoContent();
                }
                
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CatController>
        [HttpPut]        
        [Route("edit")]
        [Authorize]
        public IActionResult Put([FromBody] PutUserRequest user)
        {
            try
            {
                _userService.Update(_mapper.Map<BLL.Entities.Account>(user));
                return Ok("Пользователь изменен изменен");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CatController>
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("delete")]
        public IActionResult Delete([FromBody] DeleteUserRequest user)
        {
            try
            {
                _userService.Delete(_mapper.Map<BLL.Entities.Account>(user));
                return Ok("Пользователь удален :(");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
