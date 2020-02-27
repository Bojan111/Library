using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Core;
using Library.Data;
using LibraryMVCProjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryMVCProjects.Controllers
{
	public class BookMVCController : Controller
	{
		private readonly IBook book;
		private readonly IAuthor author;

		public BookMVCController(IBook book, IAuthor author)
		{
			this.book = book;
			this.author = author;
		}
		public IActionResult Index(string SearchTerm)
		{
			var model = new BookViewModel();
			model.Books = book.GetBooks(SearchTerm);
			return View(model);
		}
		public IActionResult Detail(int Id)
		{
			var books = book.GetBookById(Id);
			if (books == null)
			{
				return View("NotFound");
			}
			return View(books);
		}
		[HttpGet]
		public IActionResult Edit(int? Id)
		{
			var model = new BookEditModel();
			if (Id.HasValue)
			{
				model.Book = book.GetBookById(Id.Value);
				if (model.Book == null)
				{
					return RedirectToPage("./NotFound");
				}
			}
			else
			{
				model.Book = new Book();
			}

			var authors = author.GetAuthors().ToList().Select(p => new { Id = p.Id, Display = $"{p.FirstName} {p.LastName}" });
			model.Authors = new SelectList(authors, "Id", "Display");
			return View(model);
		}
		[HttpPost]
		public IActionResult Edit(BookEditModel model)
		{
			if (ModelState.IsValid)
			{
				var authoR = author.GetAuthorById(model.Book.AuthorId.Value);
				model.Book.Author = authoR;

				if (model.Book.Id == 0)
				{
					model.Book = book.Create(model.Book);
					TempData["Message"] = "The Object is created!";
				}
				else
				{
					model.Book = book.Update(model.Book);
					TempData["Message"] = "The Object is updated!";
				}

				book.Commit();
				return RedirectToAction("Index");
			}

			var authors = author.GetAuthors().ToList().Select(p => new { Id = p.Id, Display = $"{p.FirstName} {p.LastName}" });
			model.Authors = new SelectList(authors, "Id", "Display");
			return View(model);
		}
	}
}
