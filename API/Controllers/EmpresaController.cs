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
public class EmpresaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EmpresaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpresaPDto>>> Get()
    {
        var empresas = await _unitOfWork.Empresas.GetAllAsync();
        return _mapper.Map<List<EmpresaPDto>>(empresas);
    }

    [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<EmpresaPDto>>> Get([FromQuery] Params EmpresaParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Empresas.GetAllAsync(EmpresaParams.PageIndex,EmpresaParams.PageSize,EmpresaParams.Search);
        var listaEmpresa = _mapper.Map<List<EmpresaPDto>>(registros);
        return new Pager<EmpresaPDto>(listaEmpresa,totalRegistros,EmpresaParams.PageIndex,EmpresaParams.PageSize,EmpresaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Post(EmpresaPDto empresaDto)
    {
        var empresa = _mapper.Map<Empresa>(empresaDto);
        _unitOfWork.Empresas.Add(empresa);
        await _unitOfWork.SaveAsync();
        if (empresa == null)
        {
            return BadRequest();
        }
        return "Empresa Creado con Éxito!";
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] EmpresaPDto empresaDto)
    {
        if (empresaDto == null|| id != empresaDto.Id)
        {
            return BadRequest();
        }
        var empresaExiste = await _unitOfWork.Empresas.GetByIdAsync(id);

        if (empresaExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(empresaDto, empresaExiste);
        _unitOfWork.Empresas.Update(empresaExiste);
        await _unitOfWork.SaveAsync();

        return "Empresa Actualizado con Éxito!";
    } 

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var empresa = await _unitOfWork.Empresas.GetByIdAsync(id);
        if (empresa == null)
        {
            return NotFound();
        }
        _unitOfWork.Empresas.Remove(empresa);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"Se eliminó con éxito." });
    }
}