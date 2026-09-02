using LibraryManagement.Constants;
using LibraryManagement.Data;
using LibraryManagement.DTOs.Member;
using LibraryManagement.Results;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services {
    public class MemberService(LibraryManagementDbContext context) {
        public async Task<Result<MemberResponseDto>> GetByIdAsync(int id) {
            var Member = await context.Members
                .Where(m => m.Id == id)
                .Select(m => new MemberResponseDto(
                    m.Id,
                    m.Name,
                    m.Email,
                    m.RegisteredAt

                    )).FirstOrDefaultAsync();

            return Member is null ? Result<MemberResponseDto>.Failure(new Error(ErrorCodes.NotFound, "Member was not found."))
                : Result<MemberResponseDto>.Success(Member);

        }

        public async Task<Result<List<MemberResponseDto>>> GetAllAsync() {
            var Member = await context.Members
                .Select(m => new MemberResponseDto(
                    m.Id,
                    m.Name,
                    m.Email,
                    m.RegisteredAt
                    )).ToListAsync();

            return Result<List<MemberResponseDto>>.Success(Member);
        }

        
    }
}
