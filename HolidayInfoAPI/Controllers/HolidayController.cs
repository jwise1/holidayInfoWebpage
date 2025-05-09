using Microsoft.AspNetCore.Mvc;
using HolidayInfoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace HolidayInfoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]

    // Holiday Controller for handling holiday based DB accessing and API calls
    public class HolidayController : ControllerBase
    {
        // DB context for accessing the database
        private readonly ApplicationDbContext _dbContext;

        // Constructor to initialize the DB context
        public HolidayController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        // Call ApplicationDBContext to get holiday data
        [HttpGet("holidays")]
        public async Task<IActionResult> GetHolidays([FromQuery] int year, [FromQuery] string countryCode)
        {
            var holidays = await _dbContext.GetHolidaysAsync(year.ToString(), countryCode);

            return Ok(holidays);
        }

        // Call ApplicationDBContext to save holiday data
        [HttpPost("holidays")]
        public async Task<IActionResult> SaveHolidays([FromBody] List<Holiday> holidays)
        {
            try
            {
                if (holidays == null || !holidays.Any())
                {
                    return BadRequest("No holidays provided.");
                }

                _dbContext.Holidays.AddRange(holidays);
                await _dbContext.SaveChangesAsync();
                return Ok("Holidays saved successfully.");
            }
            catch (Exception)
            {
                // Log the error
                return StatusCode(500, "An error occurred while saving holidays.");
            }
        }
    }
}