using CitationWebAPI.Data;
using CitationWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitationController : ControllerBase
    {
        private readonly CitationContext _context;
        public CitationController(CitationContext context)
        {
            _context = context;
        }

        // Request all citations from database
        [HttpGet]
        public async Task<ActionResult<List<Citation>>> GetCitations()
        {
            return Ok(await _context.Citations.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Citation>>> CreateCitation(Citation citation)
        {
            _context.Citations.Add(citation);
            await _context.SaveChangesAsync(); // Adds citation to database
            
            return Ok(await _context.Citations.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Citation>>> UpdateCitation(Citation citation)
        {
            var dbCitation = await _context.Citations.FindAsync(citation.Id); // Check if citation already exists in database
            if (dbCitation == null)
                return BadRequest("Citation not found.");

            dbCitation.Name = citation.Name;

            await _context.SaveChangesAsync(); // Save updated citation

            return Ok(await _context.Citations.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Citation>>> DeleteCitation(int id)
        {
            var dbCitation = await _context.Citations.FindAsync(id);
            if (dbCitation == null)
                return BadRequest("Citation not found.");

            _context.Citations.Remove(dbCitation); // Delete citation
            await _context.SaveChangesAsync();

            return Ok(await _context.Citations.ToListAsync());
        }
    }
}
