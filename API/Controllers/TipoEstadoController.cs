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
public class TipoEstadoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipoEstadoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoEstadoPDto>>> Get()
    {
        var tipoEstados = await _unitOfWork.TipoEstados.GetAllAsync();
        return _mapper.Map<List<TipoEstadoPDto>>(tipoEstados);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoEstadoPDto>>> Get([FromQuery] Params TipoEstadoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.TipoEstados.GetAllAsync(TipoEstadoParams.PageIndex,TipoEstadoParams.PageSize,TipoEstadoParams.Search);
        var listaTipoEstado = _mapper.Map<List<TipoEstadoPDto>>(registros);
        return new Pager<TipoEstadoPDto>(listaTipoEstado,totalRegistros,TipoEstadoParams.PageIndex,TipoEstadoParams.PageSize,TipoEstadoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(TipoEstadoPDto tipoEstadoDto)
    {
        var tipoEstado = _mapper.Map<TipoEstado>(tipoEstadoDto);
        _unitOfWork.TipoEstados.Add(tipoEstado);
        await _unitOfWork.SaveAsync();
        if (tipoEstado == null)
        {
            return BadRequest();
        }
        return "TipoEstado Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] TipoEstadoPDto tipoEstadoDto)
    {
        if (tipoEstadoDto == null|| id != tipoEstadoDto.Id)
        {
            return BadRequest();
        }
        var tipoEstadoExiste = await _unitOfWork.TipoEstados.GetByIdAsync(id);

        if (tipoEstadoExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(tipoEstadoDto, tipoEstadoExiste);
        _unitOfWork.TipoEstados.Update(tipoEstadoExiste);
        await _unitOfWork.SaveAsync();

        return "TipoEstado Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var tipoEstado = await _unitOfWork.TipoEstados.GetByIdAsync(id);
        if (tipoEstado == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoEstados.Remove(tipoEstado);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}