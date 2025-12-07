using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using GestionCitasMedicas.API.Application.Interfaces;
using GestionCitasMedicas.API.Application.DTOs.Paciente;

namespace GestionCitasMedicas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly IPacienteService _pacienteService;

    public PacientesController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> GetAll()
    {
        var list = await _pacienteService.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PacienteDto>> GetById(int id)
    {
        var p = await _pacienteService.GetByIdAsync(id);
        if (p == null) return NotFound(new { message = $"Paciente {id} no encontrado" });
        return Ok(p);
    }

    [HttpPost]
    public async Task<ActionResult<PacienteDto>> Create([FromBody] CreatePacienteDto dto)
    {
        var created = await _pacienteService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PacienteDto>> Update(int id, [FromBody] UpdatePacienteDto dto)
    {
        var updated = await _pacienteService.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _pacienteService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

    [HttpGet("search/{name}")]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> SearchByName(string name)
    {
        var list = await _pacienteService.SearchByNameAsync(name);
        return Ok(list);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<PacienteDto>> GetByEmail(string email)
    {
        var p = await _pacienteService.GetByEmailAsync(email);
        if (p == null) return NotFound();
        return Ok(p);
    }
}
