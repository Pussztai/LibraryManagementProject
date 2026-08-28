using LibraryManagement.Contracts;
using LibraryManagement.Data;
using LibraryManagement.DTOs.Book;
using LibraryManagement.Entities;
using LibraryManagement.Results;

using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services {
    public class BookService(LibraryManagementDbContext context) : IBookService {
        public async Task<Result<BookResponseDto>> GetByIdAsync(int id) {
            try {
                var book = await context.Books
                    .Where(b => b.Id == id)
                    .Select(h => new BookResponseDto(
                        h.Id,
                        h.Title,
                        h.Author.FirstName + " " + h.Author.LastName,
                        h.PublishedYear,
                        h.AvalaibleCopies
                    ))
                    .FirstOrDefaultAsync();

                return book is null
                    ? Result<BookResponseDto>.Failure(new Error("Not Found", "Country was not found."))
                    : Result<BookResponseDto>.Success(book);
            } catch (Exception) {
                return Result<BookResponseDto>.Failure();
            }
        }

        public async Task<Result<IEnumerable<BookResponseDto>>> GetAllAsync() {
            var books = await context.Books.Select(h => new BookResponseDto(
                        h.Id,
                        h.Title,
                        h.Author.FirstName + " " + h.Author.LastName,
                        h.PublishedYear,
                        h.AvalaibleCopies
                    ))
                .ToListAsync();
            return Result<IEnumerable<BookResponseDto>>.Success(books);
        }

        public async Task<Result<BookResponseDto>> CreateAsync(CreateBookDto dto) {
            var isHaveAuthor = await context.Authors.AnyAsync(a => a.Id == dto.AuthorId);

            if (!isHaveAuthor) {
                return Result<BookResponseDto>.Failure(new Error("NotFound", "The author is not found"));
            }



            var newBook = new Book {
                Title = dto.Title,
                AuthorId = dto.AuthorId,
                PublishedYear = dto.PublishedYear,
                TotalCopies = dto.TotalCopies,
                AvalaibleCopies = dto.TotalCopies
            };

            context.Books.Add(newBook);
            await context.SaveChangesAsync();

            var response = new BookResponseDto(
                newBook.Id,
                newBook.Title,
                $"{newBook.Author?.FirstName} {newBook.Author?.LastName}",
                newBook.PublishedYear,
                newBook.AvalaibleCopies
            );

            return Result<BookResponseDto>.Success(response);
        }

        public async Task<Result<BookResponseDto>> UpdateAsync(int id, UpdateBookDto dto) {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book is null) {
                return Result<BookResponseDto>.NotFound(new Error("NotFound", "The book is not found"));
            }
            var BorrowedCopies = book.TotalCopies - book.AvalaibleCopies;
            if (dto.TotalCopies < BorrowedCopies) {
                return Result<BookResponseDto>.Failure();
            }

            book.AvalaibleCopies = dto.TotalCopies - book.TotalCopies;
            book.Title = dto.Title;
            book.PublishedYear = dto.PublishedYear;
            book.TotalCopies = dto.TotalCopies;
            await context.SaveChangesAsync();
            var response = new BookResponseDto(
                book.Id,
                book.Title,
                $"{book.Author?.FirstName} {book.Author?.LastName}",
                book.PublishedYear,
                book.AvalaibleCopies
            );
            return Result<BookResponseDto>.Success(response);
        }

        public async Task<Result> DeleteAsync(int id) {
            var book = await context.Books.FindAsync(id);

            if (book is null) {
                return Result.Failure(new Error("Not Found", "Book was not found."));
            }
            if (await context.Loans.AnyAsync(x => x.BookId == id && x.ReturnedAt == null)) {
                return Result.Failure(new Error("Conflict", "The book has not been returned"));
            }


            context.Books.Remove(book);
            await context.SaveChangesAsync();

            return Result.Success();
        }



        public async Task<bool> BookExistAsnyc(int id) {
            return await context.Books.AnyAsync(b => b.Id == id);
        }
    }
}
