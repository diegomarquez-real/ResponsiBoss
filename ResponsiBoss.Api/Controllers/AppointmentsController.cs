using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResponsiBoss.Api.Models;
using ResponsiBoss.Api.Models.Create;
using ResponsiBoss.Api.Models.Update;
using ResponsiBoss.Api.Services.Abstractions;

namespace ResponsiBoss.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(ILogger<AppointmentsController> logger,
            IAppointmentService appointmentService)
        {
            _logger = logger;
            _appointmentService = appointmentService;
        }

        [HttpGet("{id}", Name = "GetAppointment")]
        [ProducesResponseType(typeof(AppointmentModel), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> GetAppointmentAsync([FromRoute] Guid id)
        {
            try
            {
                var appointment = await _appointmentService.FindByIdAsync(id);

                if (appointment == null)
                    return NotFound();

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Appointment.");

                return BadRequest();
            }
        }

        [HttpGet(Name = "GetAppointments")]
        [ProducesResponseType(typeof(List<AppointmentModel>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> GetAppointmentsAsync()
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsAsync();

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Get Appointments.");

                return BadRequest();
            }
        }

        [HttpPost(Name = "CreateAppointment")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] CreateAppointmentModel createAppointmentModel)
        {
            try
            {
                var result = await _appointmentService.CreateAppointmentAsync(createAppointmentModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Create Appointment.");

                return BadRequest();
            }
        }

        [HttpPut("{id}", Name = "UpdateAppointment")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public async Task<IActionResult> UpdateAppointmentAsync([FromRoute] Guid id, [FromBody] UpdateAppointmentModel updateAppointmentModel)
        {
            try
            {
                var appointment = await _appointmentService.FindByIdAsync(id);

                if (appointment == null)
                    return NotFound();

                await _appointmentService.UpdateAppointmentAsync(appointment, updateAppointmentModel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Update Appointment.");

                return BadRequest();
            }
        }

        [HttpDelete("{id}", Name = "DeleteAppointment")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> DeleteAppointmentAsync([FromRoute] Guid id)
        {
            try
            {
                await _appointmentService.DeleteAppointmentAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed To Delete Appointment.");

                return BadRequest();
            }
        }
    }
}