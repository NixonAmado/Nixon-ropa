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
public class DepartamentoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DepartamentoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DepartamentoPDto>>> Get()
    {
        var departamentos = await _unitOfWork.Departamentos.GetAllAsync();
        return _mapper.Map<List<DepartamentoPDto>>(departamentos);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<DepartamentoPDto>>> Get([FromQuery] Params DepartamentoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Departamentos.GetAllAsync(DepartamentoParams.PageIndex,DepartamentoParams.PageSize,DepartamentoParams.Search);
        var listaDepartamento = _mapper.Map<List<DepartamentoPDto>>(registros);
        return new Pager<DepartamentoPDto>(listaDepartamento,totalRegistros,DepartamentoParams.PageIndex,DepartamentoParams.PageSize,DepartamentoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(DepartamentoPDto departamentoDto)
    {
        var departamento = _mapper.Map<Departamento>(departamentoDto);
        _unitOfWork.Departamentos.Add(departamento);
        await _unitOfWork.SaveAsync();
        if (departamento == null)
        {
            return BadRequest();
        }
        return "Departamento Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] DepartamentoPDto departamentoDto)
    {
        if (departamentoDto == null|| id != departamentoDto.Id)
        {
            return BadRequest();
        }
        var departamentoExiste = await _unitOfWork.Departamentos.GetByIdAsync(id);

        if (departamentoExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(departamentoDto, departamentoExiste);
        _unitOfWork.Departamentos.Update(departamentoExiste);
        await _unitOfWork.SaveAsync();

        return "Departamento Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var departamento = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if (departamento == null)
        {
            return NotFound();
        }
        _unitOfWork.Departamentos.Remove(departamento);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}