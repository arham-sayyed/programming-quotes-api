using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProgrammingQuotesApi.Models;
using ProgrammingQuotesApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace ProgrammingQuotesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class QuotesController : ControllerBase
    {
        public QuotesController()
        {
        }

        /// <summary>
        /// Returns an array of quotes
        /// </summary>
        [HttpGet]
        public ActionResult<List<Quote>> GetQuotes([FromQuery] int count = 0)
        {
            return QuotesService.GetQuotes(count);
        }

        /// <summary>
        /// Returns a quote for a given id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Quote> Get(string id)
        {
            Quote quote = QuotesService.Get(id);

            if(quote == null)
                return NotFound();

            return Ok(quote);
        }

        /// <summary>
        /// Returns a random quote
        /// </summary>
        [HttpGet("random")]
        public ActionResult<Quote> GetRandom() => Ok(QuotesService.GetRandom());

        /// <summary>
        /// Create new quote
        /// </summary>
        [HttpPost]
        public ActionResult Create([FromBody] Quote quote)
        {            
            QuotesService.Add(quote);
            // return CreatedAtAction(nameof(Create), new { id = quote.Id }, quote);
            return Created("", quote);
        }

        /// <summary>
        /// Returns all quotes for a given author
        /// </summary>
        [HttpGet("author/{author}")]
        public ActionResult<List<Quote>> GetQuotesByAuthor(string author) => QuotesService.GetByAuthor(author);

        /// <summary>
        /// Replace an existing quote with a new one
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(string id, Quote quote)
        {
            if (id != quote.Id)
                return BadRequest();

            Quote existingQuote = QuotesService.Get(id);
            if (existingQuote is null)
                return NotFound();

            QuotesService.Update(quote);           

            return NoContent();
        }

        /// <summary>
        /// Update certain properties of an existing quote
        /// </summary>
        [HttpPatch("{id}")]
        public ActionResult Patch(string id, JsonPatchDocument<Quote> patch)
        {
            Quote quote = QuotesService.Get(id);
            if (quote is null)
                return NotFound();

            patch.ApplyTo(quote);
            QuotesService.Update(quote);

            return NoContent();
        }
    
        /// <summary>
        /// Delete an existing quote by id
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Quote quote = QuotesService.Get(id);

            if (quote is null)
                return NotFound();

            QuotesService.Delete(id);

            return NoContent();
        }
    }
}