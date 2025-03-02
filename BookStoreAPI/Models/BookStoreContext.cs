using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Models
{
    public class BookStoreContext : IdentityDbContext
    {
        public BookStoreContext()
        {
        }
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
        }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderDetails>()
                .HasKey(od => new { od.OrderId, od.BookId });
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole("Admin") { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole("Customer") { Name = "Customer", NormalizedName = "CUSTOMER" }
            );
        }

    }
}
