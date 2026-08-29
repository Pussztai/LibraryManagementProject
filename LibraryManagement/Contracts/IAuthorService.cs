using LibraryManagement.DTOs.Author;
using LibraryManagement.Results;

namespace LibraryManagement.Contracts {
    public interface IAuthorService {
        Task<Result<AuthorResponseDto>> CreateAsync(CreateAuthorDto dto);
        Task<Result> DeleteByIdAsync(int id);
        Task<Result<IEnumerable<AuthorResponseDto>>> GetAllAsync();
        Task<Result<AuthorResponseDto>> GetByIdAsync(int id);
        Task<bool> IsAuthorExitsIdAsync(int id);
        Task<bool> IsAuthorExitsNameAsync(string name);
        Task<Result> UpdateAsync(int id, UpdateAuthorDto dto);
    }
}