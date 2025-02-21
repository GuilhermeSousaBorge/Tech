using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tech.Api.Service;
using Tech.Api.Service.LoggedUser;

namespace Tech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CheckoutsController : ControllerBase
    {
        [HttpPost]
        [Route("{bookId}")]
        public IActionResult BookCheckout(Guid bookId)
        {
            var loggedUser = new LoggedUserService(HttpContext);

            var useCase = new RegisterBookCheckoutService(loggedUser);

            useCase.Execute(bookId);

            return NoContent();
        }
    }
}
