using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Core;
using Library.Data;
using LibraryMVCProjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVCProjects.Controllers
{
    [Route("api/Books")]
    [ApiController]
    public class BookApiController : ControllerBase
    {
        private readonly IBook book;

        public BookApiController(IBook book)
        {
            this.book = book;
        }
        [HttpGet]
        public IActionResult GetBooksAll()
        {
            var data = book.GetBooks();
            return Ok(data);
        }
        [HttpGet("{Id}", Name = "GetBooks")]
        public IActionResult GetBooks(int Id)
        {
            var data = book.GetBookById(Id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpPost]
        public IActionResult Create(BookDTO  bookDTO)
        {
            if (bookDTO == null)
            {
                return BadRequest();
            }
            var books = new Book();
            books.AuthorId = bookDTO.AuthorId;
            books.Title = bookDTO.Title;
            book.Create(books);
            book.Commit();
            return CreatedAtRoute("GetBooks", new { Id = books.Id }, books);
        }
        [HttpPut("{Id}")]
        public IActionResult Update(BookDTO bookDTO, int Id)
        {
            var books = book.GetBookById(Id);
            books.AuthorId = bookDTO.AuthorId;
            books.Title = books.Title;
          
            book.Update(books);
            book.Commit();

            return NoContent();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var temp = book.Delete(Id);
            if (temp == null)
            {
                return BadRequest();
            }
            book.Commit();
            return NoContent();
        }
    }
}