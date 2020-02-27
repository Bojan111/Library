using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Core
{
	public class Author
	{
		public int Id { get; set; }
		[Required]
		[StringLength(10)]
		public string FirstName { get; set; }
		[Required]
		[StringLength(10)]
		public string LastName { get; set; }
	}
}
