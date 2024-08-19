using API.Entities;
using API.UserService.Model;
using API.UserService.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.UserService.Controller
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [Tags("Users")]
        [HttpGet("{IDCARD}", Name = "GetUserById")]
        public async Task<ActionResult<UserDto>> GetUserById([Required] string IDCARD)
        {
            try
            {
                var entity = await _repo.GetUserDetails(IDCARD);
                var userDto = _mapper.Map<UserDto>(entity);

                if (userDto != null)
                {
                    return Ok(userDto);
                }

                return NotFound($"User with ID: {IDCARD} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching user data.");
            }
        }

        [Tags("Users")]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto userRegisterDto)
        {
            var user = _mapper.Map<User>(userRegisterDto);
            _repo.Register(user);
            _repo.SaveChanges();

            var userAdded = _mapper.Map<UserDto>(user);

            return CreatedAtRoute("GetUserById", new { IDCARD = userAdded.IDCARD }, userAdded);
        }

        [Tags("Users")]
        [HttpPost("Login")]
        public async Task<LoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            var user = await _repo.Login(userLoginDto);

            if (user != null)
            {
                return new LoginResponseDto
                {
                    success = true
                };
            }
            else
            {
                return new LoginResponseDto
                {
                    success = false
                };
            }
        }
    }
}
