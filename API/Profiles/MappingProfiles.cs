using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<Cargo,CargoPDto>().ReverseMap();
        CreateMap<Cliente,ClientePDto>().ReverseMap();
        CreateMap<Cliente,ClienteOrdenDto>().ReverseMap();//
        
        CreateMap<Color,ColorPDto>().ReverseMap();
        CreateMap<Departamento,DepartamentoPDto>().ReverseMap();
        CreateMap<DetalleOrden,DetalleOrdenPDto>().ReverseMap();
        CreateMap<DetalleOrden,DetalleOrdenEndCuatroDto>().ReverseMap();//

        CreateMap<DetalleVenta,DetalleVentaPDto>().ReverseMap();
        CreateMap<Empleado,EmpleadoPDto>().ReverseMap();
        CreateMap<Empleado,EmpleadoVentaDto>().ReverseMap();
        
        CreateMap<Empresa,EmpresaPDto>().ReverseMap();
        CreateMap<Estado,EstadoPDto>().ReverseMap();
        CreateMap<Estado,EstadoADto>().ReverseMap();//

        CreateMap<FormaPago,FormaPagoPDto>().ReverseMap();
        CreateMap<Genero,GeneroPDto>().ReverseMap();
        CreateMap<Insumo,InsumoPDto>().ReverseMap();

        CreateMap<Inventario,InventarioPDto>().ReverseMap();
        CreateMap<Inventario,InventarioPrendaDto>().ReverseMap();
        CreateMap<Inventario,InventarioTallaDto>().ReverseMap();
        
        CreateMap<Municipio,MunicipioPDto>().ReverseMap();
        CreateMap<Municipio,MunicipioNombreDto>().ReverseMap();//
        
        CreateMap<Orden,OrdenPDto>().ReverseMap();
        CreateMap<OrdenCliente,OrdenClienteDto>().ReverseMap();//

        CreateMap<Pais,PaisPDto>().ReverseMap();
        CreateMap<Prenda,PrendaPDto>().ReverseMap();
        CreateMap<Prenda,PrendaOrdenDto>().ReverseMap();
        CreateMap<Prenda,PrendaNombreDto>().ReverseMap();

        CreateMap<Proveedor,ProveedorPDto>().ReverseMap();
        CreateMap<Talla,TallaPDto>().ReverseMap();
        CreateMap<TipoEstado,TipoEstadoPDto>().ReverseMap();
        CreateMap<TipoPersona,TipoPersonaPDto>().ReverseMap();
        CreateMap<TipoProteccion,TipoProteccionPDto>().ReverseMap();
        CreateMap<Venta,VentaPDto>().ReverseMap();
        CreateMap<Venta,VentaEmpleadoDto>()
        .ForMember(e => e.NroFactura, op => op.MapFrom(e => e.Id))
        .ReverseMap();
        CreateMap<VentaEmpleado,VentaEmpleadoDto>().ReverseMap();
        
        // CreateMap<Pet, FullPetDto>().ReverseMap();
        // CreateMap<Pet, PetStatDto>()
        // .ForMember(e => e.Breed, op => op.MapFrom(e => e.Breed.Name))
        // .ForMember(e => e.Species, op => op.MapFrom(e => e.Species.Name))

    }
}