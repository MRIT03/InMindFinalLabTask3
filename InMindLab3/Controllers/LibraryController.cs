using InMindLab1.Services;
using Microsoft.AspNetCore.Mvc;

namespace InMindLab1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryController : Controller
{
    private readonly ILibraryService _libraryService;

    public LibraryController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }
    
    [HttpGet("[action]")]
    public IActionResult GetBooks([FromQuery] int page, [FromQuery] int page_size)
    {
        return Ok(_libraryService.getBooks(page, page_size));
    }
    
    [HttpGet("[action]")]
    public IActionResult GetBooksByYear([FromQuery] int year, [FromQuery] string order_by)
    {
        return Ok(_libraryService.booksInYear(year, order_by));
    }
    
    [HttpGet("[action]")]
    public IActionResult GetAuthorsInYear([FromQuery] int year)
    {
        return Ok(_libraryService.authorsInYear(year));
    }
    
    [HttpGet("[action]")]
    public IActionResult GetAuthorsInYearAndCountry([FromQuery] int year, [FromQuery] string country)
    {
        return Ok(_libraryService.authorsInYearAndCountry(year, country));
    }
    
    [HttpGet("[action]")]
    public IActionResult GetTotalBooks()
    {
        return Ok(_libraryService.totalBooks());
    }
}