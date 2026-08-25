using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.DTOs.Author {
    public class CreateAuthorDto {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Range(1000, 2026)]
        public int BirthYear { get; set; }
    }
}
