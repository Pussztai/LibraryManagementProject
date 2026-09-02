using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Entities;
using LibraryManagement.Data;
using LibraryManagement.Controllers;
using LibraryManagement.Contracts;
using LibraryManagement.DTOs.Loan;

[Route("api/[controller]")]
[ApiController]
public class LoansController(ILoanService loanService) : BaseApiController
{

    // GET: api/Loan/5
    [HttpGet("{id}")]
    public async Task<ActionResult<List<LoanResponseDto>>> GetLoan(int id)
    {
        var loan = await loanService.GetByMemberIdAsync(id);
        return ToActionResult(loan);
    }

   

    // POST: api/Loan
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<LoanResponseDto>> GetLoanByMember(CreateLoanDto dto)
    {
        var result = await loanService.CreateAsync(dto);
        if (!result.IsSuccess) return MapErrorsToResponse(result.Errors);
        

        return CreatedAtAction(nameof(GetLoan), new { id = result.Value!.Id }, result);
    }

    [HttpPost("{id}/return")]

    public async Task<IActionResult> ReturnLoan(int id) {
        var result = await loanService.ReturnAsync(id);
        if (!result.IsSuccess) return MapErrorsToResponse(result.Errors);

        return NoContent();
    }
    

   
}
