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
public class ColorController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ColorController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ColorPDto>>> Get()
    {
        var colores = await _unitOfWork.Colores.GetAllAsync();
        return _mapper.Map<List<ColorPDto>>(colores);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ColorPDto>>> Get([FromQuery] Params ColorParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Colores.GetAllAsync(ColorParams.PageIndex,ColorParams.PageSize,ColorParams.Search);
        var listaColor = _mapper.Map<List<ColorPDto>>(registros);
        return new Pager<ColorPDto>(listaColor,totalRegistros,ColorParams.PageIndex,ColorParams.PageSize,ColorParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(ColorPDto colorDto)
    {
        var color = _mapper.Map<Color>(colorDto);
        _unitOfWork.Colores.Add(color);
        await _unitOfWork.SaveAsync();
        if (color == null)
        {
            return BadRequest();
        }
        return "Color Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] ColorPDto colorDto)
    {
        if (colorDto == null|| id != colorDto.Id)
        {
            return BadRequest();
        }
        var colorExiste = await _unitOfWork.Colores.GetByIdAsync(id);

        if (colorExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(colorDto, colorExiste);
        _unitOfWork.Colores.Update(colorExiste);
        await _unitOfWork.SaveAsync();

        return "Color Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var color = await _unitOfWork.Colores.GetByIdAsync(id);
        if (color == null)
        {
            return NotFound();
        }
        _unitOfWork.Colores.Remove(color);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}