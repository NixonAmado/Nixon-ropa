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
public class ProveedorController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProveedorController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProveedorPDto>>> Get()
    {
        var proveedores = await _unitOfWork.Proveedores.GetAllAsync();
        return _mapper.Map<List<ProveedorPDto>>(proveedores);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProveedorPDto>>> Get([FromQuery] Params ProveedorParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Proveedores.GetAllAsync(ProveedorParams.PageIndex,ProveedorParams.PageSize,ProveedorParams.Search);
        var listaProveedor = _mapper.Map<List<ProveedorPDto>>(registros);
        return new Pager<ProveedorPDto>(listaProveedor,totalRegistros,ProveedorParams.PageIndex,ProveedorParams.PageSize,ProveedorParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(ProveedorPDto proveedorDto)
    {
        var proveedor = _mapper.Map<Proveedor>(proveedorDto);
        _unitOfWork.Proveedores.Add(proveedor);
        await _unitOfWork.SaveAsync();
        if (proveedor == null)
        {
            return BadRequest();
        }
        return "Proveedor Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] ProveedorPDto proveedorDto)
    {
        if (proveedorDto == null|| id != proveedorDto.Id)
        {
            return BadRequest();
        }
        var proveedorExiste = await _unitOfWork.Proveedores.GetByIdAsync(id);

        if (proveedorExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(proveedorDto, proveedorExiste);
        _unitOfWork.Proveedores.Update(proveedorExiste);
        await _unitOfWork.SaveAsync();

        return "Proveedor Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var proveedor = await _unitOfWork.Proveedores.GetByIdAsync(id);
        if (proveedor == null)
        {
            return NotFound();
        }
        _unitOfWork.Proveedores.Remove(proveedor);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}