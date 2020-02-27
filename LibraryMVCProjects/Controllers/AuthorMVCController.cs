using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Core;
using Library.Data;
using LibraryMVCProjects.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryMVCProjects.Controllers
{
	public class AuthorMVCController : Controller
	{
		private readonly IAuthor author;

		public AuthorMVCController(IAuthor author)
		{
			this.author = author;
		}
		public IActionResult Index(string SearchTerm)
		{
			var model = new AuthorViewModel();
			model.Authors = author.GetAuthors(SearchTerm);
			return View(model);
		}
		public IActionResult Detail(int Id)
		{
			var authors = author.GetAuthorById(Id);
			if (authors == null)
			{
				return View("NotFound");
			}
			return View(authors);
		}
		[HttpGet]
		public IActionResult Edit(int? Id)
		{
			var model = new AuthorEditModel();
			if (Id.HasValue)
			{
				model.Author = author.GetAuthorById(Id.Value);
				if (model.Author == null)
				{
					return RedirectToPage("NotFound");
				}
			}
			else
			{
				model.Author = new Author();
			}
			return View(model);
		}
		[HttpPost]
		public IActionResult Edit(AuthorEditModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Author.Id == 0)
				{
					model.Author = author.Create(model.Author);
					TempData["Message"] = "The Object is created!";
				}
				else
				{
					model.Author = author.Update(model.Author);
					TempData["Message"] = "The Object is updated!";
				}
				author.Commit();
				return RedirectToAction("Index");
			}
			return View(model);
		}
		//[HttpGet]
		//public IActionResult Delete(int? Id)
		//{
		//	var model = new AuthorEditModel();
		//	if (Id == null)
		//	{
		//		return RedirectToPage("NotFound");
		//	}
		//	model.Author = author.GetAuthorById(Id.Value);
		//	if (model.Author == null)
		//	{
		//		return RedirectToPage("NotFound");
		//	}
		//	return View(model);
		//}
		//[HttpPost]
		//public IActionResult Delete(AuthorEditModel model)
		//{
		//	if (model.Author != null)
		//	{
		//		author.Delete(model.Author);
		//		author.Commit();

		//		TempData["DeleteMessage"] = "Author successfully deleted!";

		//		return RedirectToPage("Index");
		//	}
		//	return View(model);
		//}
	}
}
