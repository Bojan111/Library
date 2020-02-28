using Library.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMVCProjects.Models
{
	public class BookDTO
	{
		[Required, MaxLength(50), Display(Name = "Book Title")]
		public string Title { get; set; }

		public int? AuthorId { get; set; }
	}
}
