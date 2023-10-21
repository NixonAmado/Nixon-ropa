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
[Authorize(Roles = "Empleado, Administrador")]
public class OrdenController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OrdenController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("GetAllOrdenesbyIdCliente/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OrdenClienteDto>>> GetAllOrdenesbyIdCliente(int idCliente)
    {
        var ordenes = await _unitOfWork.Ordenes.GetAllOrdenesbyIdCliente(idCliente);
        return _mapper.Map<List<OrdenClienteDto>>(ordenes);

    }    


    [Authorize(Roles = "Administrador")]
    [HttpGet("GetAllOrdenesProduccionByEstado/{estado}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OrdenClienteDto>>> GetAllOrdenesProduccionByEstado(string estado)
    {
        var ordenes = await _unitOfWork.Ordenes.GetAllOrdenesProduccionByEstado(estado);
        return _mapper.Map<List<OrdenClienteDto>>(ordenes);

    }   
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OrdenPDto>>> Get()
    {
        var ordenes = await _unitOfWork.Ordenes.GetAllAsync();
        return _mapper.Map<List<OrdenPDto>>(ordenes);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<OrdenPDto>>> Get([FromQuery] Params OrdenParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Ordenes.GetAllAsync(OrdenParams.PageIndex,OrdenParams.PageSize,OrdenParams.Search);
        var listaOrden = _mapper.Map<List<OrdenPDto>>(registros);
        return new Pager<OrdenPDto>(listaOrden,totalRegistros,OrdenParams.PageIndex,OrdenParams.PageSize,OrdenParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(OrdenPDto ordenDto)
    {
        var orden = _mapper.Map<Orden>(ordenDto);
        _unitOfWork.Ordenes.Add(orden);
        await _unitOfWork.SaveAsync();
        if (orden == null)
        {
            return BadRequest();
        }
        return "Orden Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] OrdenPDto ordenDto)
    {
        if (ordenDto == null|| id != ordenDto.Id)
        {
            return BadRequest();
        }
        var ordenExiste = await _unitOfWork.Ordenes.GetByIdAsync(id);

        if (ordenExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(ordenDto, ordenExiste);
        _unitOfWork.Ordenes.Update(ordenExiste);
        await _unitOfWork.SaveAsync();

        return "Orden Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var orden = await _unitOfWork.Ordenes.GetByIdAsync(id);
        if (orden == null)
        {
            return NotFound();
        }
        _unitOfWork.Ordenes.Remove(orden);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}