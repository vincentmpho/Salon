namespace Salon.API.Models
{
    public class Appointment
    {
        public  int Id { get; set; }
        public DateTime Date { get; set; }
        public int  CustomerId { get; set; }
        public int ServiceId { get; set; }

        //Navigation Properties
        public Customer Customer { get; set; }
        public Service Service { get; set; }
    }
}
