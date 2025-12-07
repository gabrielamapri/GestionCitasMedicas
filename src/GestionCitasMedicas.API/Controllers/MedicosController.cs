using Microsoft.AspNetCore.Mvc;
using GestionCitasMedicas.API.Application.Interfaces;
using GestionCitasMedicas.API.Application.DTOs.Medico;

namespace GestionCitasMedicas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicosController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicoDto>>> GetAll()
        {
            var list = await _medicoService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicoDto>> GetById(int id)
        {
            var m = await _medicoService.GetByIdAsync(id);
            if (m == null) return NotFound(new { message = $"Medico {id} no encontrado" });
            return Ok(m);
        }

        [HttpPost]
        public async Task<ActionResult<MedicoDto>> Create([FromBody] CreateMedicoDto dto)
        {
            var created = await _medicoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicoDto>> Update(int id, [FromBody] UpdateMedicoDto dto)
        {
            var updated = await _medicoService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _medicoService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("especialidad/{especialidadId}")]
        public async Task<ActionResult<IEnumerable<MedicoDto>>> ByEspecialidad(int especialidadId)
        {
            var list = await _medicoService.SearchByEspecialidadAsync(especialidadId);
            return Ok(list);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<MedicoDto>> GetByEmail(string email)
        {
            var m = await _medicoService.GetByEmailAsync(email);
            if (m == null) return NotFound();
            return Ok(m);
        }
    }
}
