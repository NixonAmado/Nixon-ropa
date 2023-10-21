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
public class InsumoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public InsumoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("GetAllInsumosByPrenda/{prenda}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InsumoPDto>>> GetAllInsumosByPrenda(int IdPrenda)
    {
        var insumos = await _unitOfWork.Insumos.GetAllInsumosByPrenda(IdPrenda);
        return _mapper.Map<List<InsumoPDto>>(insumos);
    }
    
    [Authorize(Roles = "Administrador")]
    [HttpGet("GetAllByProvedorTipoPersona/{proveedor}/{tipoPersona}/{Nit}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InsumoPDto>>> GetAllByProvedorTipoPersona(string proveedor, string Tpersona, int Nit)
    {
        var insumos = await _unitOfWork.Insumos.GetAllByProvedorTipoPersona(proveedor, Tpersona, Nit);
        return _mapper.Map<List<InsumoPDto>>(insumos);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InsumoPDto>>> Get()
    {
        var insumos = await _unitOfWork.Insumos.GetAllAsync();
        return _mapper.Map<List<InsumoPDto>>(insumos);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<InsumoPDto>>> Get([FromQuery] Params InsumoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Insumos.GetAllAsync(InsumoParams.PageIndex,InsumoParams.PageSize,InsumoParams.Search);
        var listaInsumo = _mapper.Map<List<InsumoPDto>>(registros);
        return new Pager<InsumoPDto>(listaInsumo,totalRegistros,InsumoParams.PageIndex,InsumoParams.PageSize,InsumoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(InsumoPDto insumoDto)
    {
        var insumo = _mapper.Map<Insumo>(insumoDto);
        _unitOfWork.Insumos.Add(insumo);
        await _unitOfWork.SaveAsync();
        if (insumo == null)
        {
            return BadRequest();
        }
        return "Insumo Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] InsumoPDto insumoDto)
    {
        if (insumoDto == null|| id != insumoDto.Id)
        {
            return BadRequest();
        }
        var insumoExiste = await _unitOfWork.Insumos.GetByIdAsync(id);

        if (insumoExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(insumoDto, insumoExiste);
        _unitOfWork.Insumos.Update(insumoExiste);
        await _unitOfWork.SaveAsync();

        return "Insumo Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var insumo = await _unitOfWork.Insumos.GetByIdAsync(id);
        if (insumo == null)
        {
            return NotFound();
        }
        _unitOfWork.Insumos.Remove(insumo);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}