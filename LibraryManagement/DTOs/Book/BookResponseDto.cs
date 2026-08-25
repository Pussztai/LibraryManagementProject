namespace LibraryManagement.DTOs.Book {
    public record BookResponseDto (
        int Id,
    string Title,
    string AuthorName,
    int PublishedYear,
    int AvailableCopies

        );
    
}
