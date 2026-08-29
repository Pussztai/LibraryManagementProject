using LibraryManagement.Constants;
using LibraryManagement.Contracts;
using LibraryManagement.Data;
using LibraryManagement.DTOs.Author;
using LibraryManagement.Entities;
using LibraryManagement.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services {
    public class AuthorService(LibraryManagementDbContext context) : IAuthorService {
        public async Task<Result<AuthorResponseDto>> GetByIdAsync(int id) {
            var author = await context.Authors
                .Where(a => a.Id == id)
                .Select(a => new AuthorResponseDto(
                    a.Id, a.FirstName, a.LastName, a.BirthYear))
                .FirstOrDefaultAsync();

            return author is null
                ? Result<AuthorResponseDto>.Failure(new Error("Not Found", $"Author with {id} not found"))
                : Result<AuthorResponseDto>.Success(author);
        }

        public async Task<Result<IEnumerable<AuthorResponseDto>>> GetAllAsync() {
            var authors = await context.Authors.Select(a => new AuthorResponseDto(
                a.Id,
                a.FirstName,
                a.LastName,
                a.BirthYear)).ToListAsync();

            return Result<IEnumerable<AuthorResponseDto>>.Success(authors);
        }

        public async Task<Result<AuthorResponseDto>> CreateAsync(CreateAuthorDto dto) {
            var exist = await IsAuthorExitsNameAsync(dto.FirstName + " " + dto.LastName);

            if (exist) {
                return Result<AuthorResponseDto>.Failure(new Error(ErrorCodes.Conflict, $"Country with name '{dto.FirstName + " " + dto.LastName}' already exists."));
            }

            var newAuthor = new Author {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthYear = dto.BirthYear

            };
            await context.AddAsync(newAuthor);
            await context.SaveChangesAsync();

            var response = new AuthorResponseDto(
                newAuthor.Id,
                newAuthor.FirstName,
                newAuthor.LastName,
                newAuthor.BirthYear
            );
            return Result<AuthorResponseDto>.Success(response);

        }

        public async Task<Result> UpdateAsync(int id, UpdateAuthorDto dto) {
            var Author = await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (Author is null) {
                return Result.Failure(new Error(ErrorCodes.NotFound, $"The author with: {id} not exist"));
            }

            Author.FirstName = dto.FirstName;
            Author.LastName = dto.LastName;
            Author.BirthYear = dto.BirthYear;

            await context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteByIdAsync(int id) {

            var author = await context.Authors.FindAsync(id);

            if (author is null) {
                return Result.Failure(new Error(ErrorCodes.NotFound, $"Author not found with {id} id"));
            }

            var hasBooks = await context.Books.AnyAsync(b => b.AuthorId == id);

            if (hasBooks) {
                return Result.Failure(new Error(ErrorCodes.Conflict, "The Author has Books"));
            }


            context.Authors.Remove(author);
            await context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<bool> IsAuthorExitsNameAsync(string name) {
            return await context.Authors.AnyAsync(a => a.FirstName + " " + a.LastName == name);
        }

        public async Task<bool> IsAuthorExitsIdAsync(int id) {
            return await context.Authors.AnyAsync(a => a.Id == id);
        }




    }
}
