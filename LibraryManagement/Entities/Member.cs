namespace LibraryManagement.Entities {
    public class Member {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime RegisteredAt { get; set; }

        public List<Loan> Loans { get; set; } = [];


    }
}
