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
//[Authorize(Roles = "Empleado, Administrador, Gerente")]
public class VentaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public VentaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("GetVentasByIdEmpleado/{IdEmpleado}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VentaEmpleadoDto>>> GetVentasByIdEmpleado( int IdEmpleado)
    {
        var ventas = await _unitOfWork.Ventas.GetVentasByIdEmpleado(IdEmpleado);
        return _mapper.Map<List<VentaEmpleadoDto>>(ventas);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VentaPDto>>> Get()
    {
        var ventas = await _unitOfWork.Ventas.GetAllAsync();
        return _mapper.Map<List<VentaPDto>>(ventas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<VentaPDto>>> Get([FromQuery] Params VentaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Ventas.GetAllAsync(VentaParams.PageIndex,VentaParams.PageSize,VentaParams.Search);
        var listaVenta = _mapper.Map<List<VentaPDto>>(registros);
        return new Pager<VentaPDto>(listaVenta,totalRegistros,VentaParams.PageIndex,VentaParams.PageSize,VentaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(VentaPDto ventaDto)
    {
        var venta = _mapper.Map<Venta>(ventaDto);
        _unitOfWork.Ventas.Add(venta);
        await _unitOfWork.SaveAsync();
        if (venta == null)
        {
            return BadRequest();
        }
        return "Venta Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] VentaPDto ventaDto)
    {
        if (ventaDto == null|| id != ventaDto.Id)
        {
            return BadRequest();
        }
        var ventaExiste = await _unitOfWork.Ventas.GetByIdAsync(id);

        if (ventaExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(ventaDto, ventaExiste);
        _unitOfWork.Ventas.Update(ventaExiste);
        await _unitOfWork.SaveAsync();

        return "Venta Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var venta = await _unitOfWork.Ventas.GetByIdAsync(id);
        if (venta == null)
        {
            return NotFound();
        }
        _unitOfWork.Ventas.Remove(venta);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}