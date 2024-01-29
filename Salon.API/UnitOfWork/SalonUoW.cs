using Salon.API.Data;
using Salon.API.Repository.Interfaces;
using Salon.API.UnitOfWork.Interfaces;

namespace Salon.API.UnitOfWork
{
    public class SalonUoW : ISalonUoW
    {
        private readonly SalonDbContext _context;
        public ISalonRepository AppointmentRepository { get; }

        public SalonUoW(SalonDbContext context, ISalonRepository appointmentRepository)
        {
            _context = context;
            AppointmentRepository = appointmentRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
