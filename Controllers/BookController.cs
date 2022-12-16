using Microsoft.AspNetCore.Mvc;
using MilleniumAspWebAPI.Database;
using MilleniumAspWebAPI.Database.Models;

namespace MilleniumAspWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookDbService _bookDbService;

        public BookController(ILogger<BookController> logger, IBookDbService bookDbService)
        {
            _logger = logger;
            _bookDbService = bookDbService;
        }

        [HttpGet("~/GetBookById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromQuery] int bookId)
        {
            try
            {
                Book result = _bookDbService.GetBookById(bookId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("~/GetAvailable")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAvailable()
        {
            try
            {
                List<Book> results = _bookDbService.GetAvailableBooks();
                return Ok(results);
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost(Name = "AddNewBook")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Book book)
        {
            if(ModelState.IsValid == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            string resultMessage = _bookDbService.AddBook(book);
            bool isOK = resultMessage == String.Empty;
            if (isOK)
            {
                _logger.LogInformation("New book added succesfully on: at {DT}",DateTime.UtcNow.ToLongTimeString());
            }
            else
            {
                _logger.LogError(resultMessage);
            }
            return isOK ? Ok(book) : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut(Name = "UpdateBook")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] Book book)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            string resultMessage = _bookDbService.UpdateBook(book);
            bool isOK = resultMessage == String.Empty;
            if (isOK)
            {
                _logger.LogInformation("New book added succesfully on: at {DT}", DateTime.UtcNow.ToLongTimeString());
            }
            else
            {
                _logger.LogError(resultMessage);
            }
            return isOK ? Ok(book) : StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpDelete(Name = "DeleteBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromQuery] int bookId)
        {
            string resultMessage = _bookDbService.DeleteBook(bookId);
            bool isOK = resultMessage == String.Empty;
            if (isOK)
            {
                _logger.LogInformation("New book added succesfully on: at {DT}", DateTime.UtcNow.ToLongTimeString());
            }
            else
            {
                _logger.LogError(resultMessage);
            }
            return isOK ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
