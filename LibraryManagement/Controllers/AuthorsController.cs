using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Entities;
using LibraryManagement.Data;
using LibraryManagement.Controllers;
using LibraryManagement.Contracts;
using LibraryManagement.DTOs.Author;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController(IAuthorService authorService) : BaseApiController
{
   

    // GET: api/Author
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> GetAuthor()
    {
         var result = await authorService.GetAllAsync();
        return ToActionResult(result);
    }

    // GET: api/Author/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorResponseDto>> GetAuthor(int id)
    {
        var author = await authorService.GetByIdAsync(id);

        return ToActionResult(author);
    }

    // PUT: api/Author/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthor(int id, UpdateAuthorDto dto)
    {
        var update = await authorService.UpdateAsync(id, dto);

        return ToActionResult(update);
    }

    // POST: api/Author
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AuthorResponseDto>> PostAuthor(CreateAuthorDto dto)
    {
        var result =await authorService.CreateAsync(dto);

        return ToActionResult(result);
        //return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
    }

    // DELETE: api/Author/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await authorService.DeleteByIdAsync(id);
        return ToActionResult(author);
    }

    
}
