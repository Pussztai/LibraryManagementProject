using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTOs.Book {
    public class UpdateBookDto {
        [Required]
        [StringLength(30)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(4)]
        public string PublishedYear { get; set; } = string.Empty;

        [Required]
        public int TotalCopies { get; set; }

    }
}
