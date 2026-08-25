using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTOs.Loan {
    public class CreateLoanDto {
        [Range(1, int.MaxValue)]
        public int BookId { get; set; }

        [Range(1, int.MaxValue)]
        public int MemberId { get; set; }
    }
}
