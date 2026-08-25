namespace LibraryManagement.DTOs.Member {
    public record MemberResponseDto(
    int Id,
    string Name,
    string Email,
    DateTime RegisteredAt
);
}
