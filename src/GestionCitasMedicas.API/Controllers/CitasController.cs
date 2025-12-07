using Microsoft.AspNetCore.Mvc;
using GestionCitasMedicas.API.Application.Interfaces;
using GestionCitasMedicas.API.Application.DTOs.Cita;

namespace GestionCitasMedicas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaDto>>> GetAll()
        {
            var list = await _citaService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CitaDto>> GetById(int id)
        {
            var c = await _citaService.GetByIdAsync(id);
            if (c == null) return NotFound(new { message = $"Cita {id} no encontrada" });
            return Ok(c);
        }

        [HttpPost]
        public async Task<ActionResult<CitaDto>> Create([FromBody] CreateCitaDto dto)
        {
            var created = await _citaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CitaDto>> Update(int id, [FromBody] CreateCitaDto dto)
        {
            var updated = await _citaService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _citaService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<ActionResult<IEnumerable<CitaDto>>> ByPaciente(int pacienteId)
        {
            var list = await _citaService.GetByPacienteAsync(pacienteId);
            return Ok(list);
        }

        [HttpGet("medico/{medicoId}")]
        public async Task<ActionResult<IEnumerable<CitaDto>>> ByMedico(int medicoId)
        {
            var list = await _citaService.GetByMedicoAsync(medicoId);
            return Ok(list);
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<CitaDto>>> ByDate(DateTime date)
        {
            var list = await _citaService.GetByDateAsync(date);
            return Ok(list);
        }

        [HttpPost("{id}/cancel")]
        public async Task<ActionResult> Cancel(int id)
        {
            var existing = await _citaService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            var ok = await _citaService.CancelAsync(id);
            if (!ok) return BadRequest(new { message = "No se puede cancelar la cita (fuera de la ventana permitida o estado inválido)" });
            return NoContent();
        }
    }
}
