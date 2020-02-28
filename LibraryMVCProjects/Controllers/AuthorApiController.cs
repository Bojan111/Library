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
    [Route("api/Authors")]
    [ApiController]
    public class AuthorApiController : ControllerBase
    {
        private readonly IAuthor author;

        public AuthorApiController(IAuthor author)
        {
            this.author = author;
        }
        [HttpGet]
        public IActionResult GetAuthorsAll()
        {
            var data = author.GetAuthors();
            return Ok(data);
        }
        [HttpGet("{Id}", Name = "GetAuthor")]
        public IActionResult GetAuthors(int Id)
        {
            var data = author.GetAuthorById(Id);
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpPost]
        public IActionResult Create(AuthorDto  authorDto)
        {
            if (authorDto == null)
            {
                return BadRequest();
            }
            var authors = new Author();
            authors.FirstName = authorDto.FirstName;
            authors.LastName = authorDto.LastName;
            author.Create(authors);
            author.Commit();
            return CreatedAtRoute("GetAuthor", new { Id = authors.Id }, authors);
        }
        [HttpPut("{Id}")]
        public IActionResult Update(AuthorDto authorDto, int Id)
        {
            var authors = author.GetAuthorById(Id);
            if(authors == null)
            {
                return BadRequest();
            }
            authors.FirstName = authorDto.FirstName;
            authors.LastName = authorDto.LastName;

            author.Update(authors);
            author.Commit();

            return NoContent();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var temp = author.Delete(Id);
            if (temp == null)
            {
                return BadRequest();
            }
            author.Commit();
            return NoContent();
        }
    }
}