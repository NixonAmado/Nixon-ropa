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
public class ClienteController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ClienteController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClientePDto>>> Get()
    {
        var clientes = await _unitOfWork.Clientes.GetAllAsync();
        return _mapper.Map<List<ClientePDto>>(clientes);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ClientePDto>>> Get([FromQuery] Params ClienteParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Clientes.GetAllAsync(ClienteParams.PageIndex,ClienteParams.PageSize,ClienteParams.Search);
        var listaCliente = _mapper.Map<List<ClientePDto>>(registros);
        return new Pager<ClientePDto>(listaCliente,totalRegistros,ClienteParams.PageIndex,ClienteParams.PageSize,ClienteParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(ClientePDto clientesDto)
    {
        var clientes = _mapper.Map<Cliente>(clientesDto);
        _unitOfWork.Clientes.Add(clientes);
        await _unitOfWork.SaveAsync();
        if (clientes == null)
        {
            return BadRequest();
        }
        return "Cliente Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] ClientePDto clientesDto)
    {
        if (clientesDto == null|| id != clientesDto.Id)
        {
            return BadRequest();
        }
        var clientesExiste = await _unitOfWork.Clientes.GetByIdAsync(id);

        if (clientesExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(clientesDto, clientesExiste);
        _unitOfWork.Clientes.Update(clientesExiste);
        await _unitOfWork.SaveAsync();

        return "Cliente Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var clientes = await _unitOfWork.Clientes.GetByIdAsync(id);
        if (clientes == null)
        {
            return NotFound();
        }
        _unitOfWork.Clientes.Remove(clientes);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}