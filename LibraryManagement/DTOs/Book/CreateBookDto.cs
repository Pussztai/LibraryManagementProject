using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTOs.Book {
    public class CreateBookDto {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(4)]
        public string PublishedYear { get; set; } = string.Empty;

        [Required]
        public int TotalCopies { get; set; }

    }
}
