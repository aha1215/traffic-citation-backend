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
        private readonly DataContext _context;
        public CitationController(DataContext context)
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
            var dbCitation = await _context.Citations.FindAsync(citation.citation_id); // Check if citation already exists in database
            if (dbCitation == null)
                return BadRequest("Citation not found.");

            // Not updating driver id or user id here. Should have been set when citation created
            dbCitation.type = citation.type;
            dbCitation.date = citation.date;
            dbCitation.time = citation.time;
            dbCitation.owner_fault = citation.owner_fault;
            dbCitation.desc = citation.desc;
            dbCitation.violation_loc = citation.violation_loc;
            dbCitation.sign_date = citation.sign_date;
            dbCitation.vin = citation.vin;
            dbCitation.vin_state = citation.vin_state;
            dbCitation.code_section = citation.code_section;
            dbCitation.officer_name = citation.officer_name;
            dbCitation.officer_badge = citation.officer_badge;

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
