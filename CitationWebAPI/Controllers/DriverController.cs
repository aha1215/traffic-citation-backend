﻿using CitationWebAPI.Data;
using CitationWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CitationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly DataContext _context;
        public DriverController(DataContext context)
        {
            _context = context;
        }

        // Request all drivers from database
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Driver>>> GetDrivers()
        {
            try
            {
                return Ok(await _context.Drivers.ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("license/{license_no}")]
        [Authorize(Roles = "Admin, Officer")]
        public async Task<ActionResult<Driver>> GetDriverByLicenseNo(string license_no)
        {
            try
            {
                var dbDriver = await _context.Drivers.FirstOrDefaultAsync(driver => driver.license_no == license_no);
                if (dbDriver == null)
                {
                    // Driver doesn't exist
                    return NotFound("Driver not found.");
                }
                return Ok(dbDriver);

            } catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Officer")]
        public async Task<ActionResult<Driver>> GetDriverById(int id)
        {
            try
            {
                var dbDriver = await _context.Drivers.FindAsync(id);
                if (dbDriver == null)
                    return NotFound("Driver not found.");

                return Ok(dbDriver);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Officer")]
        public async Task<ActionResult<Driver>> CreateDriver(Driver driver)
        {
            try
            {
                _context.Drivers.Add(driver);
                await _context.SaveChangesAsync();

                var createdDriver = await _context.Drivers.FirstOrDefaultAsync(nDriver => nDriver.driver_id == driver.driver_id);

                return Ok(createdDriver);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        [Authorize(Roles = "Admin, Officer")]
        public async Task<ActionResult<Driver>> UpdateDriver(Driver driver)
        {
            try
            {
                var dbDriver = await _context.Drivers.FindAsync(driver.driver_id); // Check if driver already exists in database
                if (dbDriver == null)
                    return NotFound("Driver not found.");

                dbDriver.driver_name = driver.driver_name;
                dbDriver.date_birth = driver.date_birth;
                dbDriver.sex = driver.sex;
                dbDriver.hair = driver.hair;
                dbDriver.eyes = driver.eyes;
                dbDriver.height = driver.height;
                dbDriver.weight = driver.weight;
                dbDriver.race = driver.race;
                dbDriver.address = driver.address;
                dbDriver.city = driver.city;
                dbDriver.state = driver.state;
                dbDriver.zip = driver.zip;
                dbDriver.license_no = driver.license_no;
                dbDriver.license_class = driver.license_class;

                await _context.SaveChangesAsync(); // Save updated driver

                return Ok(dbDriver);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Driver>> DeleteDriver(int id)
        {
            try
            {
                var dbDriver = await _context.Drivers.FindAsync(id);
                if (dbDriver == null)
                    return NotFound("Driver not found.");

                _context.Drivers.Remove(dbDriver); // Delete driver
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}