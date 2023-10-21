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
public class InventarioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public InventarioController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InventarioPDto>>> Get()
    {
        var inventarios = await _unitOfWork.Inventarios.GetAllAsync();
        return _mapper.Map<List<InventarioPDto>>(inventarios);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<InventarioPDto>>> Get([FromQuery] Params InventarioParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Inventarios.GetAllAsync(InventarioParams.PageIndex,InventarioParams.PageSize,InventarioParams.Search);
        var listaInventario = _mapper.Map<List<InventarioPDto>>(registros);
        return new Pager<InventarioPDto>(listaInventario,totalRegistros,InventarioParams.PageIndex,InventarioParams.PageSize,InventarioParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(InventarioPDto inventarioDto)
    {
        var inventario = _mapper.Map<Inventario>(inventarioDto);
        _unitOfWork.Inventarios.Add(inventario);
        await _unitOfWork.SaveAsync();
        if (inventario == null)
        {
            return BadRequest();
        }
        return "Inventario Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] InventarioPDto inventarioDto)
    {
        if (inventarioDto == null|| id != inventarioDto.Id)
        {
            return BadRequest();
        }
        var inventarioExiste = await _unitOfWork.Inventarios.GetByIdAsync(id);

        if (inventarioExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(inventarioDto, inventarioExiste);
        _unitOfWork.Inventarios.Update(inventarioExiste);
        await _unitOfWork.SaveAsync();

        return "Inventario Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var inventario = await _unitOfWork.Inventarios.GetByIdAsync(id);
        if (inventario == null)
        {
            return NotFound();
        }
        _unitOfWork.Inventarios.Remove(inventario);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}