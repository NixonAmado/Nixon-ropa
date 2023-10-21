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
public class CargoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CargoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CargoPDto>>> Get()
    {
        var cargos = await _unitOfWork.Cargos.GetAllAsync();
        return _mapper.Map<List<CargoPDto>>(cargos);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CargoPDto>>> Get([FromQuery] Params CargoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Cargos.GetAllAsync(CargoParams.PageIndex,CargoParams.PageSize,CargoParams.Search);
        var listaCargo = _mapper.Map<List<CargoPDto>>(registros);
        return new Pager<CargoPDto>(listaCargo,totalRegistros,CargoParams.PageIndex,CargoParams.PageSize,CargoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(CargoPDto cargoDto)
    {
        var cargo = _mapper.Map<Cargo>(cargoDto);
        _unitOfWork.Cargos.Add(cargo);
        await _unitOfWork.SaveAsync();
        if (cargo == null)
        {
            return BadRequest();
        }
        return "Cargo Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] CargoPDto cargoDto)
    {
        if (cargoDto == null|| id != cargoDto.Id)
        {
            return BadRequest();
        }
        var cargoExiste = await _unitOfWork.Cargos.GetByIdAsync(id);

        if (cargoExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(cargoDto, cargoExiste);
        _unitOfWork.Cargos.Update(cargoExiste);
        await _unitOfWork.SaveAsync();

        return "Cargo Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var cargo = await _unitOfWork.Cargos.GetByIdAsync(id);
        if (cargo == null)
        {
            return NotFound();
        }
        _unitOfWork.Cargos.Remove(cargo);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}