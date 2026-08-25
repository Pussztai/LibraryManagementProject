using LibraryManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data {
    public class LibraryManagementDbContext:DbContext {
        public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options):base(options) {

            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }

    }
}
