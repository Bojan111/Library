using Library.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data
{
	public interface IBook
	{
		IEnumerable<Book> GetBooksByTitle(string name = null);
		IEnumerable<Book> GetBooks(string bookTitle = null, string name = null);
		Book GetBookById(int Id);
		Book Delete(int Id);
		Book Create(Book book);

		Book Update(Book book);
		int Commit();
	}
}
