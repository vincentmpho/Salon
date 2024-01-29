
using Microsoft.AspNetCore.Mvc;
using Salon.API.Models;
using Salon.API.Models.DTOs;
using Salon.API.UnitOfWork.Interfaces;

namespace Salon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        private readonly ISalonUoW _unitOfWork;

        public SalonController(ISalonUoW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointments()
        {
            try
            {
                var appointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsAsync();
                var appointmentDtos = appointments.Select(a => new AppointmentDto { Date = a.Date, CustomerId = a.CustomerId, ServiceId = a.ServiceId }).ToList();
                return Ok(appointmentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving appointments.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointment(int id)
        {
            try
            {
                var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }
                var appointmentDto = new AppointmentDto { Date = appointment.Date, CustomerId = appointment.CustomerId, ServiceId = appointment.ServiceId };
                return Ok(appointmentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the appointment.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAppointment(AppointmentDto appointmentDto)
        {
            try
            {
                var appointment = new Appointment { Date = appointmentDto.Date, CustomerId = appointmentDto.CustomerId, ServiceId = appointmentDto.ServiceId };
                await _unitOfWork.AppointmentRepository.AddAppointmentAsync(appointment);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointmentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the appointment.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAppointment(int id, AppointmentDto appointmentDto)
        {
            try
            {
                var existingAppointment = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(id);
                if (existingAppointment == null)
                {
                    return NotFound();
                }
                existingAppointment.Date = appointmentDto.Date;
                existingAppointment.CustomerId = appointmentDto.CustomerId;
                existingAppointment.ServiceId = appointmentDto.ServiceId;
                await _unitOfWork.AppointmentRepository.UpdateAppointmentAsync(existingAppointment);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the appointment.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            try
            {
                var existingAppointment = await _unitOfWork.AppointmentRepository.GetAppointmentByIdAsync(id);
                if (existingAppointment == null)
                {
                    return NotFound();
                }
                await _unitOfWork.AppointmentRepository.DeleteAppointmentAsync(id);
                await _unitOfWork.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the appointment.");
            }
        }
    }
}

