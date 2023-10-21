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
public class EstadoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EstadoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EstadoPDto>>> Get()
    {
        var estados = await _unitOfWork.Estados.GetAllAsync();
        return _mapper.Map<List<EstadoPDto>>(estados);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<EstadoPDto>>> Get([FromQuery] Params EstadoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Estados.GetAllAsync(EstadoParams.PageIndex,EstadoParams.PageSize,EstadoParams.Search);
        var listaEstado = _mapper.Map<List<EstadoPDto>>(registros);
        return new Pager<EstadoPDto>(listaEstado,totalRegistros,EstadoParams.PageIndex,EstadoParams.PageSize,EstadoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(EstadoPDto estadoDto)
    {
        var estado = _mapper.Map<Estado>(estadoDto);
        _unitOfWork.Estados.Add(estado);
        await _unitOfWork.SaveAsync();
        if (estado == null)
        {
            return BadRequest();
        }
        return "Estado Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] EstadoPDto estadoDto)
    {
        if (estadoDto == null|| id != estadoDto.Id)
        {
            return BadRequest();
        }
        var estadoExiste = await _unitOfWork.Estados.GetByIdAsync(id);

        if (estadoExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(estadoDto, estadoExiste);
        _unitOfWork.Estados.Update(estadoExiste);
        await _unitOfWork.SaveAsync();

        return "Estado Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var estado = await _unitOfWork.Estados.GetByIdAsync(id);
        if (estado == null)
        {
            return NotFound();
        }
        _unitOfWork.Estados.Remove(estado);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}