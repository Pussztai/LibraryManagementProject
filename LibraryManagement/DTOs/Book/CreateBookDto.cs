using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTOs.Book {
    public class CreateBookDto {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int PublishedYear { get; set; }

        [Required]
        public int TotalCopies { get; set; }

    }
}
