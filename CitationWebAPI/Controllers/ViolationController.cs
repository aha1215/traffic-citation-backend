using CitationWebAPI.Data;
using CitationWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViolationController : ControllerBase
    {
        private readonly DataContext _context;
        public ViolationController(DataContext context)
        {
            _context = context;
        }

        // Request all violations from database
        [HttpGet]
        public async Task<ActionResult<List<Violation>>> GetViolations()
        {
            try
            {
                return Ok(await _context.Violations.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{citation_id}")]
        public async Task<ActionResult<List<Violation>>> GetViolationsByCitationId(int citation_id)
        {
            try
            {
                var dbViolations = await _context.Violations.Where(nViolation => nViolation.citation_id == citation_id).ToListAsync();
                if (dbViolations == null)
                    return NotFound("Citation not found.");

                return Ok(dbViolations);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        // Posts a new violation to database
        [HttpPost]
        public async Task<ActionResult<List<Violation>>> CreateViolation(Violation violation)
        {
            try
            {
                _context.Violations.Add(violation);
                await _context.SaveChangesAsync();

                return Ok(await _context.Violations.ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Citation>>> UpdateViolation(Violation violation)
        {
            try
            {
                var dbViolation = await _context.Violations.FindAsync(violation.violation_id);
                if (dbViolation == null)
                    return NotFound("Violation not found.");

                // Not updating citation id here. Should have been set when violation was created
                dbViolation.group = violation.group;
                dbViolation.code = violation.code;
                dbViolation.degree = violation.degree;
                dbViolation.desc = violation.desc;

                await _context.SaveChangesAsync(); // Save updated violation

                return Ok(await _context.Violations.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Violation>>> DeleteViolation(int id)
        {
            try
            {
                var dbViolation = await _context.Violations.FindAsync(id);
                if (dbViolation == null)
                    return NotFound("Violation not found.");

                _context.Violations.Remove(dbViolation);
                await _context.SaveChangesAsync();

                return Ok(await _context.Violations.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}

