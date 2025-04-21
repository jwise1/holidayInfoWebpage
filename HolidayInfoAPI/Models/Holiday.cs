namespace HolidayInfoAPI.Models
{
    public class Holiday
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string CountryCode { get; set; }
        public DateTime HolidayDate { get; set; }
    }
}