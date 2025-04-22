namespace HolidayInfoAPI.Models
{
    // Holiday object class for holding holiday data
    public class Holiday
    {
        // Properties of the Holiday object
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string CountryCode { get; set; }
        public DateTime Date { get; set; }
    }
}