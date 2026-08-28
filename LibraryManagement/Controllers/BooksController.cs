using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Entities;
using LibraryManagement.Data;
using LibraryManagement.Controllers;
using LibraryManagement.DTOs.Book;
using LibraryManagement.Services;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService bookService) : BaseApiController
{
    

    // GET: api/Book
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetBooks()
    {
        var books = await bookService.GetAllAsync();
        return ToActionResult(books);
    }

    // GET: api/Book/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponseDto>> GetBook(int id)
    {
        var book = await bookService.GetByIdAsync(id);

        return ToActionResult(book);
    }

    // PUT: api/Book/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, UpdateBookDto dto)
    {
        var book = await bookService.UpdateAsync(id, dto);

        return ToActionResult(book);
    }

    // POST: api/Book
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<BookResponseDto>> PostBook(CreateBookDto dto)
    {
        var result = await bookService.CreateAsync(dto);
        if (!result.IsSuccess) return MapErrorsToResponse(result.Errors);

        return CreatedAtAction(nameof(GetBook), new { id = result.Value!.Id }, result.Value);
    }

    // DELETE: api/Book/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await bookService.DeleteAsync(id);
        return ToActionResult(book);
        
    }

    
}
