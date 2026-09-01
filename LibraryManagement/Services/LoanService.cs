using System.Net;
using LibraryManagement.Constants;
using LibraryManagement.Data;
using LibraryManagement.DTOs.Loan;
using LibraryManagement.Entities;
using LibraryManagement.Results;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services {
    public class LoanService(LibraryManagementDbContext context) {
        public async Task<Result<LoanResponseDto>> CreateAsync(CreateLoanDto dto) {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == dto.BookId);
            if(book is null) {
                return Result<LoanResponseDto>.Failure(new Error(ErrorCodes.NotFound,"The book is not found with id: " + dto.BookId));
            }
            var member = await context.Members.FirstOrDefaultAsync(m => m.Id == dto.MemberId);

            if(member is null) {
                return Result<LoanResponseDto>.Failure(new Error(ErrorCodes.NotFound, "Member is not found with id: " + dto.MemberId));

            }
            var alreadyBorrowed = await context.Loans.AnyAsync(l =>
                l.BookId == dto.BookId && l.MemberId == dto.MemberId && l.ReturnedAt == null);
            if (alreadyBorrowed) {
                return Result<LoanResponseDto>.Failure(new Error(ErrorCodes.Conflict, "This member has already borrowed this book."));
            }

            if (book.AvalaibleCopies <= 0) {
                return Result<LoanResponseDto>.Failure(new Error(ErrorCodes.Conflict, "The book has no copies"));
            }
            var NewLoan = new Loan {
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                BorrowedAt = DateTime.UtcNow,
            };
            book.AvalaibleCopies -= 1;
            await context.AddAsync(NewLoan);
            await context.SaveChangesAsync();
            

            
            var response = new LoanResponseDto(
                NewLoan.Id,
                book.Id,
                book.Title,
                member.Id,
                member.Name,
                NewLoan.BorrowedAt,
                NewLoan.ReturnedAt
            );

            return Result<LoanResponseDto>.Success(response);
        }

        public async Task<Result> ReturnAsync(int loanId) {
            var loan = await context.Loans
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.Id == loanId);

            if (loan is null) {
                return Result.NotFound(new Error(ErrorCodes.NotFound, "The loan is not found"));
            }

            if (loan.ReturnedAt != null) {
                return Result.Failure(new Error(ErrorCodes.Conflict, "The book has already been returned"));
            }

            loan.ReturnedAt = DateTime.UtcNow;
            loan.Book.AvalaibleCopies += 1;

            await context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result<List<LoanResponseDto>>> GetByMemberIdAsync(int memberId) {
            var response = await context.Loans
                .Where(l => l.MemberId == memberId)
                .Select(l => new LoanResponseDto(
                    l.Id,
                    l.BookId,
                    l.Book.Title,
                    l.MemberId,
                    l.Member.Name,
                    l.BorrowedAt,
                    l.ReturnedAt
                ))
                .ToListAsync();

            return Result<List<LoanResponseDto>>.Success(response);
        }
    }

}

