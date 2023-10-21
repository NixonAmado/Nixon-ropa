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
public class FormaPagoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public FormaPagoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<FormaPagoPDto>>> Get()
    {
        var formaPago = await _unitOfWork.FormaPagos.GetAllAsync();
        return _mapper.Map<List<FormaPagoPDto>>(formaPago);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<FormaPagoPDto>>> Get([FromQuery] Params FormaPagoParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.FormaPagos.GetAllAsync(FormaPagoParams.PageIndex,FormaPagoParams.PageSize,FormaPagoParams.Search);
        var listaFormaPago = _mapper.Map<List<FormaPagoPDto>>(registros);
        return new Pager<FormaPagoPDto>(listaFormaPago,totalRegistros,FormaPagoParams.PageIndex,FormaPagoParams.PageSize,FormaPagoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(FormaPagoPDto formaPagoDto)
    {
        var formaPago = _mapper.Map<FormaPago>(formaPagoDto);
        _unitOfWork.FormaPagos.Add(formaPago);
        await _unitOfWork.SaveAsync();
        if (formaPago == null)
        {
            return BadRequest();
        }
        return "FormaPago Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] FormaPagoPDto formaPagoDto)
    {
        if (formaPagoDto == null|| id != formaPagoDto.Id)
        {
            return BadRequest();
        }
        var formaPagoExiste = await _unitOfWork.FormaPagos.GetByIdAsync(id);

        if (formaPagoExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(formaPagoDto, formaPagoExiste);
        _unitOfWork.FormaPagos.Update(formaPagoExiste);
        await _unitOfWork.SaveAsync();

        return "FormaPago Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var formaPago = await _unitOfWork.FormaPagos.GetByIdAsync(id);
        if (formaPago == null)
        {
            return NotFound();
        }
        _unitOfWork.FormaPagos.Remove(formaPago);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}