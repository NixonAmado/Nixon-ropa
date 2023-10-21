using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize(Roles = "Empleado, Administrador, Gerente")]
public class TipoPersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipoPersonaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoPersonaPDto>>> Get()
    {
        var tipoPersonas = await _unitOfWork.TipoPersonas.GetAllAsync();
        return _mapper.Map<List<TipoPersonaPDto>>(tipoPersonas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoPersonaPDto>>> Get([FromQuery] Params TipoPersonaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.TipoPersonas.GetAllAsync(TipoPersonaParams.PageIndex,TipoPersonaParams.PageSize,TipoPersonaParams.Search);
        var listaTipoPersona = _mapper.Map<List<TipoPersonaPDto>>(registros);
        return new Pager<TipoPersonaPDto>(listaTipoPersona,totalRegistros,TipoPersonaParams.PageIndex,TipoPersonaParams.PageSize,TipoPersonaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(TipoPersonaPDto tipoPersonaDto)
    {
        var tipoPersona = _mapper.Map<TipoPersona>(tipoPersonaDto);
        _unitOfWork.TipoPersonas.Add(tipoPersona);
        await _unitOfWork.SaveAsync();
        if (tipoPersona == null)
        {
            return BadRequest();
        }
        return "TipoPersona Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] TipoPersonaPDto tipoPersonaDto)
    {
        if (tipoPersonaDto == null|| id != tipoPersonaDto.Id)
        {
            return BadRequest();
        }
        var tipoPersonaExiste = await _unitOfWork.TipoPersonas.GetByIdAsync(id);

        if (tipoPersonaExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(tipoPersonaDto, tipoPersonaExiste);
        _unitOfWork.TipoPersonas.Update(tipoPersonaExiste);
        await _unitOfWork.SaveAsync();

        return "TipoPersona Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var tipoPersona = await _unitOfWork.TipoPersonas.GetByIdAsync(id);
        if (tipoPersona == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoPersonas.Remove(tipoPersona);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}