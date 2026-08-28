using LibraryManagement.Data;
using LibraryManagement.DTOs.Author;
using LibraryManagement.Results;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services {
    public class AuthorService(LibraryManagementDbContext context) {
        public async Task<Result<AuthorResponseDto>> GetByIdAsnc(int id) {
            var author = await context.Authors
                .Where(a => a.Id == id)
                .Select(a => new AuthorResponseDto(
                    a.Id, a.FirstName, a.LastName, a.BirthYear))
                .FirstOrDefaultAsync();

            return author is null
                ? Result<AuthorResponseDto>.Failure(new Error("Not Found", $"Author with {id} not found"))
                : Result<AuthorResponseDto>.Success(author);
        }


        
    }
}
