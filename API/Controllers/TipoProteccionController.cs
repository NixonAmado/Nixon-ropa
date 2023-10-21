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
public class TipoProteccionController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipoProteccionController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoProteccionPDto>>> Get()
    {
        var tipoProtecciones = await _unitOfWork.TipoProtecciones.GetAllAsync();
        return _mapper.Map<List<TipoProteccionPDto>>(tipoProtecciones);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoProteccionPDto>>> Get([FromQuery] Params TipoProteccionParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.TipoProtecciones.GetAllAsync(TipoProteccionParams.PageIndex,TipoProteccionParams.PageSize,TipoProteccionParams.Search);
        var listaTipoProteccion = _mapper.Map<List<TipoProteccionPDto>>(registros);
        return new Pager<TipoProteccionPDto>(listaTipoProteccion,totalRegistros,TipoProteccionParams.PageIndex,TipoProteccionParams.PageSize,TipoProteccionParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(TipoProteccionPDto tipoProteccionDto)
    {
        var tipoProteccion = _mapper.Map<TipoProteccion>(tipoProteccionDto);
        _unitOfWork.TipoProtecciones.Add(tipoProteccion);
        await _unitOfWork.SaveAsync();
        if (tipoProteccion == null)
        {
            return BadRequest();
        }
        return "TipoProteccionPDto Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] TipoProteccionPDto tipoProteccionDto)
    {
        if (tipoProteccionDto == null|| id != tipoProteccionDto.Id)
        {
            return BadRequest();
        }
        var tipoProteccionExiste = await _unitOfWork.TipoProtecciones.GetByIdAsync(id);

        if (tipoProteccionExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(tipoProteccionDto, tipoProteccionExiste);
        _unitOfWork.TipoProtecciones.Update(tipoProteccionExiste);
        await _unitOfWork.SaveAsync();

        return "TipoProteccionPDto Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var tipoProteccion = await _unitOfWork.TipoProtecciones.GetByIdAsync(id);
        if (tipoProteccion == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoProtecciones.Remove(tipoProteccion);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}