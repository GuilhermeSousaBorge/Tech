using Microsoft.AspNetCore.Mvc;
using Tech.Api.Service;
using Tech.Exception;
using TechDto.Request;
using TechDto.Response;

namespace Tech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(UserCreatedResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorsResponseDto), StatusCodes.Status401Unauthorized)]
        public IActionResult SignIn(SignInRequestDto request)
        {
            try
            {
                var signInService = new SignInService();

                var response = signInService.SignIn(request);

                return Ok(response);
            }
            catch (TechException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized,new ErrorsResponseDto
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
