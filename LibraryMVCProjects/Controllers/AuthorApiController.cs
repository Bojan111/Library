using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
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
        [HttpGet("{Id}")]
        public IActionResult GetAuthors(int Id)
        {
            var data = author.GetAuthorById(Id);
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}