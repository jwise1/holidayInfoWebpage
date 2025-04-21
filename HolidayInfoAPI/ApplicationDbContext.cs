using Microsoft.EntityFrameworkCore;
using HolidayInfoAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Define the DbSet for the Holidays table
    public DbSet<Holiday> Holidays { get; set; }

    // Method to query holidays
    public async Task<List<Holiday>> GetHolidaysAsync(string year, string countryCode)
    {
        // Use 'this' (the current DbContext instance) to access Holidays
        var holidays = await this.Holidays
            .Where(h => h.HolidayDate.Year.ToString() == year && h.CountryCode == countryCode)
            .GroupBy(h => h.Name) // Group by holiday name
            .Select(g => g.First()) // Select the first holiday from each group
            .ToListAsync();

        return holidays;

    }

    // Method to save holidays
    public async Task SaveHolidaysAsync(List<Holiday> holidays)
    {
        // Add holidays to the DbSet
        this.Holidays.AddRange(holidays);

        // Save changes to the database
        await this.SaveChangesAsync();
    }
}