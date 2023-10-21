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
public class PrendaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PrendaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PrendaPDto>>> Get()
    {
        var prendas = await _unitOfWork.Prendas.GetAllAsync();
        return _mapper.Map<List<PrendaPDto>>(prendas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PrendaPDto>>> Get([FromQuery] Params PrendaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Prendas.GetAllAsync(PrendaParams.PageIndex,PrendaParams.PageSize,PrendaParams.Search);
        var listaPrenda = _mapper.Map<List<PrendaPDto>>(registros);
        return new Pager<PrendaPDto>(listaPrenda,totalRegistros,PrendaParams.PageIndex,PrendaParams.PageSize,PrendaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(PrendaPDto prendasDto)
    {
        var prendas = _mapper.Map<Prenda>(prendasDto);
        _unitOfWork.Prendas.Add(prendas);
        await _unitOfWork.SaveAsync();
        if (prendas == null)
        {
            return BadRequest();
        }
        return "PrendaPDto Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] PrendaPDto prendasDto)
    {
        if (prendasDto == null|| id != prendasDto.Id)
        {
            return BadRequest();
        }
        var prendaExiste = await _unitOfWork.Prendas.GetByIdAsync(id);

        if (prendaExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(prendasDto, prendaExiste);
        _unitOfWork.Prendas.Update(prendaExiste);
        await _unitOfWork.SaveAsync();

        return "Prenda Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var prendas = await _unitOfWork.Prendas.GetByIdAsync(id);
        if (prendas == null)
        {
            return NotFound();
        }
        _unitOfWork.Prendas.Remove(prendas);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}