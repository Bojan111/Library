using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Core
{
	public class Book
	{
		public int Id { get; set; }
		[Required, MaxLength(50), Display(Name = "Book Title")]
		public string Title { get; set; }

		public Author Author { get; set; }

		public int? AuthorId { get; set; }
	}
}
