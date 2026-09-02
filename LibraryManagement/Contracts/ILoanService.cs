using LibraryManagement.DTOs.Loan;
using LibraryManagement.Results;

namespace LibraryManagement.Contracts {
    public interface ILoanService {
        Task<Result<LoanResponseDto>> CreateAsync(CreateLoanDto dto);
        Task<Result<List<LoanResponseDto>>> GetByMemberIdAsync(int memberId);
        Task<Result> ReturnAsync(int loanId);
    }
}