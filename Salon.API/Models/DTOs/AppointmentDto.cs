namespace Salon.API.Models.DTOs
{
    public class AppointmentDto
    {
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; } 
    }
}
