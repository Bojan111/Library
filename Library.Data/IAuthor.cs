using Library.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data
{
	public interface IAuthor
	{
		IEnumerable<Author> GetAuthors(string name = null);
		Author GetAuthorById(int Id);
		Author Create(Author author);
		Author Update(Author author);
		int Commit();
		Author Delete(Author author);
	}
}
