using Microsoft.AspNetCore.Mvc;
using Tech.Api.Service;
using Tech.Exception;
using TechDto.Request;
using TechDto.Response;

namespace Tech.Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(UserCreatedResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorsResponseDto), StatusCodes.Status400BadRequest)]
        public IActionResult Create(UserRequestDto request)
        {
            try
            {
                var userService = new UsersService();
                var newUser = userService.CreateUser(request);

                return Created(string.Empty, newUser);
            }
            catch (TechException ex)
            {
                return BadRequest( new ErrorsResponseDto
                {
                    Errors = ex.GetErrorMessage()
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorsResponseDto
                {
                    Errors = ["Internal Server Error"]
                });
            }
        }
    }
}
