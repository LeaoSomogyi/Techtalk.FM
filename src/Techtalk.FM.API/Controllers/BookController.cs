using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Techtalk.FM.Domain.Contracts.Repositories;
using DTO = Techtalk.FM.Domain.DTOs;
using Entities = Techtalk.FM.Domain.Entities;

namespace Techtalk.FM.API.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost]
        [Authorize("Bearer")]
        [Route("")]
        public async Task<IActionResult> Save(DTO.Book book)
        {
            return Ok(await _bookRepository.SaveAsync(new Entities.Book(book)));
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _bookRepository.GetAsync(id));
        }

        [HttpPut]
        [Authorize("Bearer")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bookRepository.DeleteAsync(id);

            return Ok();
        }
    }
}