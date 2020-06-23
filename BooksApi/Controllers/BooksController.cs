using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase {
        private readonly BookService _bookService;

        public BooksController (BookService bookService) {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get () =>
            _bookService.Get ();

        [HttpGet ("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get (string id) {
            var book = _bookService.Get (id);

            if (book == null) {
                return NotFound ();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create (Book book) {
            _bookService.Create (book);

            return CreatedAtRoute ("GetBook", new { id = book.Id.ToString () }, book);
        }

        [HttpPut ("{id:length(24)}")]
        public IActionResult Update (string id, Book bookIn) {
            var book = _bookService.Get (id);

            if (book == null) {
                return NotFound ();
            }

            _bookService.Update (id, bookIn);

            return NoContent ();
        }

        [HttpDelete ("{id:length(24)}")]
        public IActionResult Delete (string id) {
            var book = _bookService.Get (id);

            if (book == null) {
                return NotFound ();
            }

            _bookService.Remove (book.Id);

            return NoContent ();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks () {
            try {
                return await Task.Run (() => _bookService.Get ());
            } catch (Exception e) {
                throw e;
            }
        }

        [HttpGet ("{id}")]
        public async Task<Book> GetBookById (string id) {
            try {
                return await Task.Run (() => _bookService.Get (id));
            } catch (Exception e) {
                throw e;
            }
        }

        // // PUT: api/Alunos/5
        // [HttpPut ("{id}")]
        // public async Task<IActionResult> PutAluno (int id, Aluno aluno) {
        //     if (id != aluno.AlunoId) {
        //         return BadRequest ();
        //     }
        //     _context.Entry (aluno).State = EntityState.Modified;
        //     try {
        //         await _context.SaveChangesAsync ();
        //     } catch (DbUpdateConcurrencyException) {
        //         if (!AlunoExists (id)) {
        //             return NotFound ();
        //         } else {
        //             throw;
        //         }
        //     }
        //     return NoContent ();
        // }
        // // POST: api/Alunos
        // [HttpPost]
        // public async Task<ActionResult<Aluno>> PostAluno (Aluno aluno) {
        //     _context.Alunos.Add (aluno);
        //     await _context.SaveChangesAsync ();
        //     return CreatedAtAction ("GetAluno", new { id = aluno.AlunoId }, aluno);
        // }
        // // DELETE: api/Alunos/5
        // [HttpDelete ("{id}")]
        // public async Task<ActionResult<Aluno>> DeleteAluno (int id) {
        //     var aluno = await _context.Alunos.FindAsync (id);
        //     if (aluno == null) {
        //         return NotFound ();
        //     }
        //     _context.Alunos.Remove (aluno);
        //     await _context.SaveChangesAsync ();
        //     return aluno;
        // }
        // private bool AlunoExists (int id) {
        //     return _context.Alunos.Any (e => e.AlunoId == id);
        // }
    }
}