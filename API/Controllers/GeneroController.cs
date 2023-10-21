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
public class GeneroController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GeneroController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GeneroPDto>>> Get()
    {
        var generos = await _unitOfWork.Generos.GetAllAsync();
        return _mapper.Map<List<GeneroPDto>>(generos);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<GeneroPDto>>> Get([FromQuery] Params GeneroParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Generos.GetAllAsync(GeneroParams.PageIndex,GeneroParams.PageSize,GeneroParams.Search);
        var listaGenero = _mapper.Map<List<GeneroPDto>>(registros);
        return new Pager<GeneroPDto>(listaGenero,totalRegistros,GeneroParams.PageIndex,GeneroParams.PageSize,GeneroParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(GeneroPDto generoDto)
    {
        var genero = _mapper.Map<Genero>(generoDto);
        _unitOfWork.Generos.Add(genero);
        await _unitOfWork.SaveAsync();
        if (genero == null)
        {
            return BadRequest();
        }
        return "GeneroPDto Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] GeneroPDto generoDto)
    {
        if (generoDto == null|| id != generoDto.Id)
        {
            return BadRequest();
        }
        var generoExiste = await _unitOfWork.Generos.GetByIdAsync(id);

        if (generoExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(generoDto, generoExiste);
        _unitOfWork.Generos.Update(generoExiste);
        await _unitOfWork.SaveAsync();

        return "Genero Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var genero = await _unitOfWork.Generos.GetByIdAsync(id);
        if (genero == null)
        {
            return NotFound();
        }
        _unitOfWork.Generos.Remove(genero);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}