using CitationWebAPI.Data;
using CitationWebAPI.Dto;
using CitationWebAPI.Models;
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

        [HttpGet]
        public async Task<ActionResult<List<Citation>>> GetCitations()
        {
            try
            {
                return Ok(await _context.Citations.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Request a range of citations for pagination
        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<ActionResult<List<Citation>>> GetCitations(int pageNumber, float pageSize)
        {
            try
            {
                // If requesting more than 50 pages default to 50
                pageSize = pageSize > 50 ? 50 : pageSize;
                // Page number must be greater than 0
                pageNumber = pageNumber < 1 ? 1 : pageNumber + 1;

                var totalCitationsCount = await _context.Citations.CountAsync();
                var pageCount = Math.Ceiling(_context.Citations.Count() / pageSize);
                var citations = await _context.Citations
                    .Skip((pageNumber - 1) * (int)pageSize)
                    .Take((int)pageSize)
                    .ToListAsync();

                // Get the drivers associated with each citation
                var drivers = new List<Driver>();
                foreach (var element in citations)
                {
                    var driver = _context.Drivers.FindAsync(element.driver_id);
                    if (driver.Result != null)
                    {
                        drivers.Add(driver.Result);
                    }
                }

                var response = new CitationResponse
                {
                    Citations = citations,
                    Drivers = drivers,
                    TotalCitationsCount = totalCitationsCount,
                    CurrentPage = pageNumber,
                    TotalPages = (int)pageCount
                };

                return Ok(response);
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Citation>>> CreateCitation(Citation citation)
        {
            try
            {
                _context.Citations.Add(citation);
                await _context.SaveChangesAsync(); // Adds citation to database

                return Ok(await _context.Citations.ToListAsync());
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("/api/CitationWithViolations")]
        public async Task<ActionResult<List<Citation>>> CreateCitationWithViolations(CitationWithViolations citation)
        {
            try
            {
                _context.Citations.Add(citation.citation);
                await _context.SaveChangesAsync();

                int citation_id = citation.citation.citation_id; // should have newly created citation id

                foreach (var violation in citation.violations)
                {
                    violation.citation_id = citation_id;
                    _context.Violations.Add(violation);
                    await _context.SaveChangesAsync();
                }

                return Ok(await _context.Citations.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Citation>>> UpdateCitation(Citation citation)
        {
            try
            {
                var dbCitation = await _context.Citations.FindAsync(citation.citation_id); // Check if citation already exists in database
                if (dbCitation == null)
                    return NotFound("Citation not found.");

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
            catch (Exception e)
            {
                return BadRequest(e.ToString()); 
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Citation>>> DeleteCitation(int id)
        {
            try
            {
                var dbCitation = await _context.Citations.FindAsync(id);
                if (dbCitation == null)
                    return NotFound("Citation not found.");

                _context.Citations.Remove(dbCitation); // Delete citation
                await _context.SaveChangesAsync();

                return Ok(await _context.Citations.ToListAsync());
            } 
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
