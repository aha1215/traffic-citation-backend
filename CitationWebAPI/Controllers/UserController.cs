using CitationWebAPI.Data;
using CitationWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        // Request all users from database
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            try
            {
                return Ok(await _context.Users.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(await _context.Users.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult<List<User>>> UpdateUser(User user)
        {
            try
            {
                var dbUser = await _context.Users.FindAsync(user.user_id); // Check if user already exists in database
                if (dbUser == null)
                    return NotFound("User not found.");

                dbUser.username = user.username;
                dbUser.password = user.password;
                dbUser.email = user.email;
                dbUser.officer_badge = user.officer_badge;
                dbUser.officer_name = user.officer_name;  

                await _context.SaveChangesAsync(); // Save updated user

                return Ok(await _context.Users.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            try
            {
                var dbUser = await _context.Users.FindAsync(id);
                if (dbUser == null)
                    return NotFound("User not found.");

                _context.Users.Remove(dbUser); // Delete user
                await _context.SaveChangesAsync();

                return Ok(await _context.Users.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
