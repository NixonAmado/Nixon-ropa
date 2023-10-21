using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IRole Roles {get;}
        public IUser Users {get;}
        public IPrenda Prendas {get;}
        public ITipoProteccion TipoProtecciones {get;} 
        public IGenero Generos {get;}
        public IEstado Estados {get;}
        public ITipoEstado TipoEstados {get;}
        public IEmpresa Empresas {get;}
        public IMunicipio Municipios {get;}
        public IDepartamento Departamentos {get;}
        public IPais Paises {get;}
        public IInsumo Insumos {get;}
        public IProveedor Proveedores {get;}
        public ITipoPersona TipoPersonas {get;}
        public ICliente Clientes {get;}
        public IVenta Ventas {get;} 
        public IDetalleOrden DetalleOrdenes {get;}
        public IColor Colores {get;}
        public IOrden Ordenes {get;}
        public IDetalleVenta DetalleVentas {get;}
        public IEmpleado Empleados {get;}
        public ICargo Cargos {get;}
        public ITalla Tallas {get;}
        public IInventario Inventarios {get;}
        public IFormaPago FormaPagos {get;}
        Task<int> SaveAsync();
    }
}