using Library.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data
{
	public class LibraryDbContext : IdentityDbContext<IdentityUser>
	{
		public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
		{

		}
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
	}
}
