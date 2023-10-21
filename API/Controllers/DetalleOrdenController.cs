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
public class DetalleOrdenController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DetalleOrdenController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetalleOrdenPDto>>> Get()
    {
        var detalleOrdenes = await _unitOfWork.DetalleOrdenes.GetAllAsync();
        return _mapper.Map<List<DetalleOrdenPDto>>(detalleOrdenes);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<DetalleOrdenPDto>>> Get([FromQuery] Params DetalleOrdenParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.DetalleOrdenes.GetAllAsync(DetalleOrdenParams.PageIndex,DetalleOrdenParams.PageSize,DetalleOrdenParams.Search);
        var listaDetalleOrden = _mapper.Map<List<DetalleOrdenPDto>>(registros);
        return new Pager<DetalleOrdenPDto>(listaDetalleOrden,totalRegistros,DetalleOrdenParams.PageIndex,DetalleOrdenParams.PageSize,DetalleOrdenParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(DetalleOrdenPDto detalleOrdenDto)
    {
        var detalleOrden = _mapper.Map<DetalleOrden>(detalleOrdenDto);
        _unitOfWork.DetalleOrdenes.Add(detalleOrden);
        await _unitOfWork.SaveAsync();
        if (detalleOrden == null)
        {
            return BadRequest();
        }
        return "DetalleOrden Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] DetalleOrdenPDto detalleOrdenDto)
    {
        if (detalleOrdenDto == null|| id != detalleOrdenDto.Id)
        {
            return BadRequest();
        }
        var detalleOrdenExiste = await _unitOfWork.DetalleOrdenes.GetByIdAsync(id);

        if (detalleOrdenExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(detalleOrdenDto, detalleOrdenExiste);
        _unitOfWork.DetalleOrdenes.Update(detalleOrdenExiste);
        await _unitOfWork.SaveAsync();

        return "DetalleOrden Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var detalleOrden = await _unitOfWork.DetalleOrdenes.GetByIdAsync(id);
        if (detalleOrden == null)
        {
            return NotFound();
        }
        _unitOfWork.DetalleOrdenes.Remove(detalleOrden);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}