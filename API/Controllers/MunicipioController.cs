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
public class MunicipioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public MunicipioController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MunicipioPDto>>> Get()
    {
        var municipios = await _unitOfWork.Municipios.GetAllAsync();
        return _mapper.Map<List<MunicipioPDto>>(municipios);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MunicipioPDto>>> Get([FromQuery] Params MunicipioParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Municipios.GetAllAsync(MunicipioParams.PageIndex,MunicipioParams.PageSize,MunicipioParams.Search);
        var listaMunicipio = _mapper.Map<List<MunicipioPDto>>(registros);
        return new Pager<MunicipioPDto>(listaMunicipio,totalRegistros,MunicipioParams.PageIndex,MunicipioParams.PageSize,MunicipioParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(MunicipioPDto municipioDto)
    {
        var municipio = _mapper.Map<Municipio>(municipioDto);
        _unitOfWork.Municipios.Add(municipio);
        await _unitOfWork.SaveAsync();
        if (municipio == null)
        {
            return BadRequest();
        }
        return "Municipio Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] MunicipioPDto municipioDto)
    {
        if (municipioDto == null|| id != municipioDto.Id)
        {
            return BadRequest();
        }
        var municipioExiste = await _unitOfWork.Municipios.GetByIdAsync(id);

        if (municipioExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(municipioDto, municipioExiste);
        _unitOfWork.Municipios.Update(municipioExiste);
        await _unitOfWork.SaveAsync();

        return "Municipio Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var municipio = await _unitOfWork.Municipios.GetByIdAsync(id);
        if (municipio == null)
        {
            return NotFound();
        }
        _unitOfWork.Municipios.Remove(municipio);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}