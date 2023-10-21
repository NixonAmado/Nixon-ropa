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
public class TallaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TallaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TallaPDto>>> Get()
    {
        var tallas = await _unitOfWork.Tallas.GetAllAsync();
        return _mapper.Map<List<TallaPDto>>(tallas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TallaPDto>>> Get([FromQuery] Params TallaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Tallas.GetAllAsync(TallaParams.PageIndex,TallaParams.PageSize,TallaParams.Search);
        var listaTalla = _mapper.Map<List<TallaPDto>>(registros);
        return new Pager<TallaPDto>(listaTalla,totalRegistros,TallaParams.PageIndex,TallaParams.PageSize,TallaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(TallaPDto tallaDto)
    {
        var talla = _mapper.Map<Talla>(tallaDto);
        _unitOfWork.Tallas.Add(talla);
        await _unitOfWork.SaveAsync();
        if (talla == null)
        {
            return BadRequest();
        }
        return "Talla Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] TallaPDto tallaDto)
    {
        if (tallaDto == null|| id != tallaDto.Id)
        {
            return BadRequest();
        }
        var tallaExiste = await _unitOfWork.Tallas.GetByIdAsync(id);

        if (tallaExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(tallaDto, tallaExiste);
        _unitOfWork.Tallas.Update(tallaExiste);
        await _unitOfWork.SaveAsync();

        return "Talla Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var talla = await _unitOfWork.Tallas.GetByIdAsync(id);
        if (talla == null)
        {
            return NotFound();
        }
        _unitOfWork.Tallas.Remove(talla);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}