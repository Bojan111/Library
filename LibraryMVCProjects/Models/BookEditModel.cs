using Library.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMVCProjects.Models
{
	public class BookEditModel
	{
		public IEnumerable<SelectListItem> Authors { get; set; }
		public Book Book { get; set; }
	}
}
