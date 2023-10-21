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
public class DetalleVentaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DetalleVentaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetalleVentaPDto>>> Get()
    {
        var detalleVentas = await _unitOfWork.DetalleVentas.GetAllAsync();
        return _mapper.Map<List<DetalleVentaPDto>>(detalleVentas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<DetalleVentaPDto>>> Get([FromQuery] Params DetalleVentaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.DetalleVentas.GetAllAsync(DetalleVentaParams.PageIndex,DetalleVentaParams.PageSize,DetalleVentaParams.Search);
        var listaDetalleVenta = _mapper.Map<List<DetalleVentaPDto>>(registros);
        return new Pager<DetalleVentaPDto>(listaDetalleVenta,totalRegistros,DetalleVentaParams.PageIndex,DetalleVentaParams.PageSize,DetalleVentaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(DetalleVentaPDto detalleVentaDto)
    {
        var detalleVenta = _mapper.Map<DetalleVenta>(detalleVentaDto);
        _unitOfWork.DetalleVentas.Add(detalleVenta);
        await _unitOfWork.SaveAsync();
        if (detalleVenta == null)
        {
            return BadRequest();
        }
        return "DetalleVenta Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] DetalleVentaPDto detalleVentaDto)
    {
        if (detalleVentaDto == null|| id != detalleVentaDto.Id)
        {
            return BadRequest();
        }
        var detalleVentaExiste = await _unitOfWork.DetalleVentas.GetByIdAsync(id);

        if (detalleVentaExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(detalleVentaDto, detalleVentaExiste);
        _unitOfWork.DetalleVentas.Update(detalleVentaExiste);
        await _unitOfWork.SaveAsync();

        return "DetalleVenta Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var detalleVenta = await _unitOfWork.DetalleVentas.GetByIdAsync(id);
        if (detalleVenta == null)
        {
            return NotFound();
        }
        _unitOfWork.DetalleVentas.Remove(detalleVenta);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}