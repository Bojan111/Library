using Library.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data
{
	public class LibraryDbContext : DbContext
	{
		public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
		{

		}
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
	}
}
