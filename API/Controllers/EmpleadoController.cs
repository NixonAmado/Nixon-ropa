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
public class EmpleadoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EmpleadoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    

    [HttpGet("GetAllEmpleadosByCargo/{cargo}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpleadoPDto>>> GetAllEmpleadosByCargo(string Cargo)
    {
        var empleado = await _unitOfWork.Empleados.GetAllEmpleadosByCargo(Cargo);
        return _mapper.Map<List<EmpleadoPDto>>(empleado);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpleadoPDto>>> Get()
    {
        var empleado = await _unitOfWork.Empleados.GetAllAsync();
        return _mapper.Map<List<EmpleadoPDto>>(empleado);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<EmpleadoPDto>>> Get([FromQuery] Params EmpleadoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Empleados.GetAllAsync(EmpleadoParams.PageIndex,EmpleadoParams.PageSize,EmpleadoParams.Search);
        var listaEmpleado = _mapper.Map<List<EmpleadoPDto>>(registros);
        return new Pager<EmpleadoPDto>(listaEmpleado,totalRegistros,EmpleadoParams.PageIndex,EmpleadoParams.PageSize,EmpleadoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(EmpleadoPDto empleadoDto)
    {
        var empleado = _mapper.Map<Empleado>(empleadoDto);
        _unitOfWork.Empleados.Add(empleado);
        await _unitOfWork.SaveAsync();
        if (empleado == null)
        {
            return BadRequest();
        }
        return "Empleado Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] EmpleadoPDto empleadoDto)
    {
        if (empleadoDto == null|| id != empleadoDto.Id)
        {
            return BadRequest();
        }
        var empleadoExiste = await _unitOfWork.Empleados.GetByIdAsync(id);

        if (empleadoExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(empleadoDto, empleadoExiste);
        _unitOfWork.Empleados.Update(empleadoExiste);
        await _unitOfWork.SaveAsync();

        return "Empleado Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
        if (empleado == null)
        {
            return NotFound();
        }
        _unitOfWork.Empleados.Remove(empleado);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}