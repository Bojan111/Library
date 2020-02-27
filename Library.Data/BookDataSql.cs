using Library.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data
{
	public class BookDataSql : IBook
	{
		private readonly LibraryDbContext libraryDbContext;

		public BookDataSql(LibraryDbContext libraryDbContext)
		{
			this.libraryDbContext = libraryDbContext;
		}
		public int Commit()
		{
			return libraryDbContext.SaveChanges();
		}

		public Book Create(Book book)
		{
			var books = libraryDbContext.Books.Add(book);
			return books.Entity;
		}

		public Book Delete(int Id)
		{
			var delete = libraryDbContext.Books.SingleOrDefault(r => r.Id == Id);
			if(delete != null)
			{
				libraryDbContext.Books.Remove(delete);
			}
			return delete;
		}

		public Book GetBookById(int Id)
		{
			return libraryDbContext.Books.SingleOrDefault(r => r.Id == Id);
		}

		public IEnumerable<Book> GetBooks(string bookTitle = null, string name = null)
		{
			var books = !string.IsNullOrEmpty(name) ? $"{name}" : name;
			return libraryDbContext.Books
				.Include(b => b.Author)
				.Where(r => string.IsNullOrEmpty(name) || EF.Functions.Like(r.Author.FirstName, books) || EF.Functions.Like(r.Author.LastName, books)).ToList();
		}

		public IEnumerable<Book> GetBooksByTitle(string name = null)
		{
			var books = !string.IsNullOrEmpty(name) ? $"{name}" : name;
			return libraryDbContext.Books
				.Where(r => string.IsNullOrEmpty(name) || EF.Functions.Like(r.Title, books)).ToList();
		}

		public Book Update(Book book)
		{
			libraryDbContext.Entry(book).State = EntityState.Modified;
			return book;
		}
	}
}
