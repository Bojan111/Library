using Library.Core;
using Library.Core.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Library.Data
{
	public class LibraryDbContext : IdentityDbContext<ApplicationUser>
	{
		public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
		{

		}
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
	}
}
