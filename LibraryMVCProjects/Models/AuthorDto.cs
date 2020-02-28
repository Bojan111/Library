using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMVCProjects.Models
{
	public class AuthorDto
	{
		[Required]
		[StringLength(10)]
		public string FirstName { get; set; }
		[Required]
		[StringLength(10)]
		public string LastName { get; set; }
	}
}
