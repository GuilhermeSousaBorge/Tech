using Microsoft.AspNetCore.Mvc;
using Tech.Api.Service;
using Tech.Exception;
using TechDto.Request;
using TechDto.Response;

namespace Tech.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet("filter")]
        [ProducesResponseType(typeof(BooksResponseDto), StatusCodes.Status200OK)]
        public IActionResult Filter(int pageNumber, string? title)
        {
            try
            {
                var filterService = new FilterBookService();

                var resquest = new FilterBookRequestDto
                {
                    PageNumber = pageNumber,
                    Title = title
                };

                var result = filterService.Execute(resquest);

                return Ok(result);
            }
            catch(TechException ex)
            {
                return NotFound();

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
