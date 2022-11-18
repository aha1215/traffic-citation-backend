using CitationWebAPI.Data;
using CitationWebAPI.Dto;
using CitationWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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

        // Get all citations
        [HttpGet]
        [Authorize(Roles = "Admin")] 
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

        // Get citation by id
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Officer")]
        public async Task<ActionResult<Citation>> GetCitationById(int id)
        {
            try
            {
                var dbCitation = await _context.Citations.FindAsync(id);
                if (dbCitation == null)
                    return NotFound("Citation not found.");

                return Ok(dbCitation);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        // Request a range of citations for pagination
        [HttpGet("{pageNumber}/{pageSize}/{userId}/{userRole}")]
        [Authorize(Roles = "Admin, Officer")]
        public async Task<ActionResult<List<Citation>>> GetCitations(int pageNumber, float pageSize, string userId, string userRole)
        {
            try
            {
                userId = userId.Replace("%", "|"); // TODO: change this

                // If requesting more than 50 pages default to 50
                pageSize = pageSize > 50 ? 50 : pageSize;
                // Page number must be greater than 0
                pageNumber = pageNumber < 1 ? 1 : pageNumber;

                var totalCitationsCount = 0;
                var citations = new List<Citation>();
                
                if (userRole == "Admin")
                {
                    // display all citations 
                    totalCitationsCount = await _context.Citations.CountAsync();
                    citations = await _context.Citations
                        .Skip((pageNumber - 1) * (int)pageSize)
                        .Take((int)pageSize)
                        .ToListAsync();
                } 
                else if (userRole == "Officer")
                {
                    // Find citations assigned to user by user id
                    citations =  _context.Citations.Where(citation => citation.user_id == userId)
                        .Skip((pageNumber - 1) * (int)pageSize)
                        .Take((int)pageSize)
                        .ToList();
                    totalCitationsCount = _context.Citations.Where(citation => citation.user_id == userId).Count();
                    
                }

                var pageCount = Math.Ceiling(totalCitationsCount / pageSize);

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
        [Authorize(Roles = "Admin, Officer")]
        public async Task<ActionResult<Citation>> CreateCitation(Citation citation)
        {
            try
            {
                _context.Citations.Add(citation);
                await _context.SaveChangesAsync(); // Adds citation to database

                var createdCitation = await _context.Citations.FirstOrDefaultAsync(nCitation => nCitation.citation_id == citation.citation_id);

                return Ok(createdCitation);
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("/api/CitationWithViolations")]
        [Authorize(Roles = "Admin, Officer")]
        public async Task<ActionResult<Citation>> CreateCitationWithViolations(CitationWithViolations citation)
        {
            try
            {
                _context.Citations.Add(citation.citation);
                await _context.SaveChangesAsync();

                int citation_id = citation.citation.citation_id; // should have newly created citation id
                var createdCitation = await _context.Citations.FirstOrDefaultAsync(nCitation => nCitation.citation_id == citation_id);

                foreach (var violation in citation.violations)
                {
                    violation.citation_id = citation_id;
                    _context.Violations.Add(violation);
                    await _context.SaveChangesAsync();
                }

                return Ok(createdCitation);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Officer")]
        public async Task<ActionResult<Citation>> UpdateCitation(Citation citation)
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

                var updatedCitation = await _context.Citations.FirstOrDefaultAsync(nCitation => nCitation.citation_id == dbCitation.citation_id);

                return Ok(updatedCitation);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString()); 
            }
            
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Officer")]
        public async Task<ActionResult<Citation>> DeleteCitation(int id)
        {
            try
            {
                var dbCitation = await _context.Citations.FindAsync(id);
                if (dbCitation == null)
                    return NotFound("Citation not found.");

                _context.Citations.Remove(dbCitation); // Delete citation
                await _context.SaveChangesAsync();

                return Ok("Successfully deleted citation");
            } 
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
