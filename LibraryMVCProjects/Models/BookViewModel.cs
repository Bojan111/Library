using Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMVCProjects.Models
{
	public class BookViewModel
	{
		public IEnumerable<Book> Books { get; set; }
		public string SearchBook { get; set; }
		public string SearchAuthor { get; set; }
		public string Message { get; set; }
		public string TempMessage { get; set; }
	}
}
