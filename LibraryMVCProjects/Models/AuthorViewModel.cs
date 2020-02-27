using Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMVCProjects.Models
{
	public class AuthorViewModel
	{
		public IEnumerable<Author> Authors { get; set; }
		
		public string SearchTerm { get; set; }

		public string Message { get; set; }

		public string TempMessage { get; set; }
	}
}
