using Salon.API.Repository.Interfaces;

namespace Salon.API.UnitOfWork.Interfaces
{
    public interface ISalonUoW
    {
        ISalonRepository AppointmentRepository { get; }
        Task SaveAsync();
    }
}
