using LibraryManagement.DTOs.Book;
using LibraryManagement.Results;

namespace LibraryManagement.Contracts {
    public interface IBookService {
        Task<bool> BookExistAsnyc(int id);
        Task<Result<BookResponseDto>> CreateAsync(CreateBookDto dto);
        Task<Result> DeleteAsync(int id);
        Task<Result<IEnumerable<BookResponseDto>>> GetAllAsync();
        Task<Result<BookResponseDto>> GetByIdAsync(int id);
        Task<Result<BookResponseDto>> UpdateAsync(int id, UpdateBookDto dto);
    }
}