using Library.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data
{
	public class AuthorDataSql : IAuthor
	{
		private readonly LibraryDbContext libraryDbContext;

		public AuthorDataSql(LibraryDbContext libraryDbContext)
		{
			this.libraryDbContext = libraryDbContext;
		}
		public int Commit()
		{
			return libraryDbContext.SaveChanges();
		}

		public Author Create(Author author)
		{
			var temp = libraryDbContext.Authors.Add(author);
			return temp.Entity;
		}

		public Author Delete(int Id)
		{
			var temp = libraryDbContext.Authors.SingleOrDefault(r => r.Id == Id);
			if(temp != null)
			{
				libraryDbContext.Authors.Remove(temp);
			}
			return temp;
		}

		public Author GetAuthorById(int Id)
		{
			return libraryDbContext.Authors.SingleOrDefault(r => r.Id == Id);
		}

		public IEnumerable<Author> GetAuthors(string name = null)
		{
			var authors = !string.IsNullOrEmpty(name) ? $"{name}" : name;
			return libraryDbContext.Authors.Where(r => string.IsNullOrEmpty(name)
					|| EF.Functions.Like(r.FirstName, authors)
					|| EF.Functions.Like(r.LastName, authors)).ToList();
		}

		public Author Update(Author author)
		{
			libraryDbContext.Entry(author).State = EntityState.Modified;
			return author;
		}
	}
}
