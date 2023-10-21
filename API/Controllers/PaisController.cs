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
public class PaisController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PaisController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PaisPDto>>> Get()
    {
        var pais = await _unitOfWork.Paises.GetAllAsync();
        return _mapper.Map<List<PaisPDto>>(pais);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PaisPDto>>> Get([FromQuery] Params PaisParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Paises.GetAllAsync(PaisParams.PageIndex,PaisParams.PageSize,PaisParams.Search);
        var listaPais = _mapper.Map<List<PaisPDto>>(registros);
        return new Pager<PaisPDto>(listaPais,totalRegistros,PaisParams.PageIndex,PaisParams.PageSize,PaisParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(PaisPDto paisDto)
    {
        var pais = _mapper.Map<Pais>(paisDto);
        _unitOfWork.Paises.Add(pais);
        await _unitOfWork.SaveAsync();
        if (pais == null)
        {
            return BadRequest();
        }
        return "Pais Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] PaisPDto paisDto)
    {
        if (paisDto == null|| id != paisDto.Id)
        {
            return BadRequest();
        }
        var paisExiste = await _unitOfWork.Paises.GetByIdAsync(id);

        if (paisExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(paisDto, paisExiste);
        _unitOfWork.Paises.Update(paisExiste);
        await _unitOfWork.SaveAsync();

        return "Pais Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var pais = await _unitOfWork.Paises.GetByIdAsync(id);
        if (pais == null)
        {
            return NotFound();
        }
        _unitOfWork.Paises.Remove(pais);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}